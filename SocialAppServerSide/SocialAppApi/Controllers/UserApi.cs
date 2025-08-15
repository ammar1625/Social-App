using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SocialAppApi.Models;
using SocialAppBusinessLayer;
using SocialAppBusinessLayer.Utiles;
using SocialAppDataLayer.Dtos;
using StackExchange.Redis;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using SocialAppApi.Utiles;

namespace SocialAppApi.Controllers
{
    [Route("api/Users")]
    [ApiController]
    [Authorize]
    [AllowAnonymous]
    public class UserApi : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConnectionMultiplexer _redis;
        public UserApi(IWebHostEnvironment env,IConnectionMultiplexer redis)
        {
            _env = env;
            _redis = redis;
        }
        [HttpGet("GetById/{UserId}",Name ="GetUserById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult>GetUserbyId(string UserId)
        {
            if (UserId == null || string.IsNullOrWhiteSpace(UserId)) 
            {
                return BadRequest("invalid data");
            }

            clsUser? User  = await clsUser.GetUserByIdAsync(UserId);
            if (User == null)
            {
                return NotFound($"user with id {UserId} is not found");
            }

            return Ok(User.userdto);

        }
        
        [HttpDelete("delete-User")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteUserAsync(DeleteUserModel Model)
        {
            if (Model == null || string.IsNullOrEmpty(Model.Email) || string.IsNullOrEmpty(Model.PassWord)||
                string.IsNullOrWhiteSpace(Model.Email)|| string.IsNullOrWhiteSpace(Model.PassWord)) 
            {
                return BadRequest("invalid data");
            }

            clsUser? User = await clsUser.GetUserByEmailAsync(Model.Email);

            if(User==null)
            {
                return NotFound("user not found");
            }

            bool IsPassWordVerified = clsUtils.VerifyPassWord(Model.PassWord , User.PassWord);

            bool IsDeleted = false;

            if (IsPassWordVerified)
            {
                 IsDeleted = await clsUser.DeleteUserAsync(Model.Email);
            }


            return Ok(new {IsDeleted = IsDeleted });
        }

        [HttpPut("change-password")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ChangePassWordAsync(ChangePasswordModel Model)
        {
            if (Model == null
                || string.IsNullOrEmpty(Model.Email)
                || string.IsNullOrWhiteSpace(Model.Email)
                || string.IsNullOrWhiteSpace(Model.NewPassWord)
                || string.IsNullOrEmpty(Model.NewPassWord))
            {
                return BadRequest("invalid data");
            }

            clsUser? User = await clsUser.GetUserByEmailAsync(Model.Email);

            if (User != null) 
            {
                User.PassWord = Model.NewPassWord;
            }
         
            if(await User.ResetPassWordAsync())
            {
               
                return Ok(new {status = true , User = User.userdto});
            }
            return Ok(new { status = false, User = User.userdto });
        }
        
        [HttpPut("update-credentials")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateUserCredentials(UpdateUserDto Model)
        {
            if(Model == null ||string.IsNullOrEmpty(Model.FirstName)|| string.IsNullOrWhiteSpace(Model.FirstName)||
                string.IsNullOrEmpty(Model.LastName)|| string.IsNullOrWhiteSpace(Model.LastName)||
                string.IsNullOrEmpty(Model.UserName)|| string.IsNullOrWhiteSpace(Model.UserName)||
                Model.DateOfBirth > DateTime.Now || string.IsNullOrEmpty(Model.Email)|| string.IsNullOrWhiteSpace(Model.Email)||
                string.IsNullOrEmpty(Model.Phone)||string.IsNullOrWhiteSpace(Model.Phone)|| Model.Gender == null)
            {
                return BadRequest("invalid data");
            }

            clsUser? User = await clsUser.GetUserByIdAsync(Model.UserId);

            if(User == null)
            {
                return NotFound($"User with id {Model.UserId} is not found");
            }

            User.FirstName = Model.FirstName;
            User.LastName = Model.LastName;
            User.UserName = Model.UserName;
            User.Email = Model.Email;
            User.Phone = Model.Phone;
            User.DateOfBirth = Model.DateOfBirth;
            User.Gender = Model.Gender;
           
            if (await User.UpdateUserCredentialsAsync())
            {
                return Ok(User.userdto);
            }
            return
                StatusCode(500, "internal server error");
        }
       
        [HttpPut("update-profile-pic")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateUserProfilePicAsync(UpdateProfilePicModel Model)
        {
            if (Model.ProfilePic == null || Model.ProfilePic.Length == 0)
            {
                return BadRequest("no image uploaded");
            }
            string FolderName = "Profile-Pics";
            var UploadsFolder = Path.Combine(_env.WebRootPath, FolderName);

            if (!Directory.Exists(UploadsFolder))
            {
                Directory.CreateDirectory(UploadsFolder);
            }

            var FileName = Guid.NewGuid() + Path.GetExtension(Model.ProfilePic.FileName);
            var FilePath = Path.Combine(UploadsFolder, FileName);

            using (FileStream Stream = new FileStream(FilePath, FileMode.Create))
            {
                await Model.ProfilePic.CopyToAsync(Stream);
            }

            var ProfilePicUrl = $"{Request.Scheme}://{Request.Host}/{FolderName}/{FileName}";

            clsUser? User = await clsUser.GetUserByIdAsync(Model.UserId);

            if(User == null)
            {
                return BadRequest($"user with id {Model.UserId} is not found");
            }
            User.ProfilePic = ProfilePicUrl;
            if(await User.UpdateProfilePicAsync())
            {
                return Ok(User.userdto.ProfilePic);

            }
            return StatusCode(500,"internal server error");



        }

        [HttpGet("users-list")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetUsersListASync(string NameFilter)
        {
            if(NameFilter == null || string.IsNullOrEmpty(NameFilter)|| string.IsNullOrWhiteSpace(NameFilter))
            {
                return BadRequest("invalid data");
            }

            List<UserDto> Users = await clsUser.GetUsersListAsync(NameFilter);
            return Ok(Users);
        }

        [HttpPost("verify-password")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult>VerifyPassWordAsync(VerifyPassWordModel Model)
        {
            if(Model==null||string.IsNullOrEmpty(Model.PassWord)||string.IsNullOrWhiteSpace(Model.PassWord))
            {
                return BadRequest("invalid data");
            }

            clsUser? User = await clsUser.GetUserByIdAsync(Model.UserId);
            if (User == null) 
            {
                return NotFound($"user with id {Model.UserId} is not found");
            }
            //verify if the sent password equivalent to the stored password hash in the data base
            bool IsValidPassWord = clsUtils.VerifyPassWord(Model.PassWord, User.PassWord);

            return Ok(new {IsValid =  IsValidPassWord });
        }

        [HttpPost("log-out")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult>LogOutAsync([FromBody]string UserId)
        {
            if(string.IsNullOrEmpty(UserId)||string.IsNullOrWhiteSpace(UserId))
            {
                return BadRequest("invalid data");
            }
            string? AuthHeader = Request.Headers.Authorization.ToString();
            if(string.IsNullOrEmpty(AuthHeader)||!AuthHeader.StartsWith("Bearer "))
            {
                return Unauthorized();
            }

            string AccessToken = AuthHeader.Substring("Bearer ".Length).Trim();

            if(!Utils.TryGetTokenExpiry(AccessToken, out DateTime Expiry))
            {
                return BadRequest("could not read toke, expiry time");
            }

            //delete all refresh token for this user
            await clsRefreshToken.DeleteAllRefreshTokensByUserIdAsync(UserId);

            var TimeToExpiry = Expiry - DateTime.UtcNow;
            if(TimeToExpiry <= TimeSpan.Zero)
            {
                return Ok(new { IsLoggedOut = true });
            }

            var Db = _redis.GetDatabase();
            await Db.StringSetAsync(
                key:$"blacklisted_token:{AccessToken}",
                value:true,
                expiry:TimeToExpiry
                );

            return Ok(new { IsLoggedOut = true });
        }

    }
}

using ApiAuthenticationAndSecurity.User_Management;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SocialAppApi.Models;
using SocialAppApi.Tokens;
using SocialAppBusinessLayer;
using SocialAppDataLayer.Dtos;
using SocialAppDataLayer.Models;

namespace SocialAppApi.Controllers
{
    [Route("api/Authentication")]
    [ApiController]
    [Authorize]
    [AllowAnonymous]
    public class AuthenticationController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IemailService _emailService;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        public AuthenticationController(IMapper mapper, IemailService emailService, IConfiguration configuration,
            IWebHostEnvironment env)
        {
            _mapper = mapper;
            _emailService = emailService;
            _configuration = configuration;
            _env = env;
        }

        [HttpPost("Register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> AddNewUser( UserModel user)
        {
            if (string.IsNullOrEmpty(user.FirstName) || string.IsNullOrWhiteSpace(user.FirstName) ||
                string.IsNullOrEmpty(user.LastName) || string.IsNullOrWhiteSpace(user.LastName) ||
                string.IsNullOrEmpty(user.UserName) || string.IsNullOrWhiteSpace(user.UserName) ||
                string.IsNullOrEmpty(user.Email) || string.IsNullOrWhiteSpace(user.Email) ||
                string.IsNullOrEmpty(user.Phone) || string.IsNullOrWhiteSpace(user.Phone) ||
                string.IsNullOrEmpty(user.PassWord) || string.IsNullOrWhiteSpace(user.PassWord) ||
                ((user.DateOfBirth == null) || (user.IsActive == null) || (user.IsEmailVerified == null))
                )
            {
                return BadRequest("invalid data");
            }

            clsUser User = new clsUser(new UserDto(Guid.NewGuid().ToString(), user.FirstName, user.LastName,user.UserName,
                user.DateOfBirth,user.Email,user.PassWord,user.Phone,null,user.IsEmailVerified,user.IsActive,user.Gender)) /*_mapper.Map<clsUser>(user)*/;

            bool IsAdded = await User.AddNewUserAsync();

            if (IsAdded)
            {
                var Token = Tokens.TokensProvider.GenerateEmailConfirmationToken(_configuration, user.Email);
                var ComfirmationLink = Url.Action(nameof(VerifyAccount), "Authentication", new { Token, email = user.Email }, Request.Scheme);
                _emailService.SendEmail(new message(new List<string>() { user.Email }, "email verification email", ComfirmationLink));
                return CreatedAtRoute("GetUserById", new { UserId = User.UserId }, User.userdto);
            }
            else
            {
                return StatusCode(500, "internal server error");
            }


        }

        [HttpGet("VerifyAccount")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> VerifyAccount(string Token, string Email)
        {
            clsUser User = await clsUser.GetUserByEmailAsync(Email);

            if (User != null)
            {
                bool IsVerified = await User.VerifyEmailAsync();

                if (IsVerified)
                {
                    return Ok("Account verifed successfully you can go to login page");
                }
                else
                {
                    return Ok("something went wrong");
                }
            }
            return Ok("something went wrong");
        }

        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login(LogInDto LoginCredentials)
        {
            if (string.IsNullOrEmpty(LoginCredentials.Email) || string.IsNullOrEmpty(LoginCredentials.PassWord) ||
                string.IsNullOrWhiteSpace(LoginCredentials.Email) || string.IsNullOrWhiteSpace(LoginCredentials.PassWord))
            {
                return BadRequest("invalid Data");
            }
            bool IsLoggedIn = await clsUser.LogInAsync(LoginCredentials.Email, LoginCredentials.PassWord);

            //check if the user with the input login credencials is found
            if (IsLoggedIn)
            {


                //generate otp code
                int Code = TokensProvider.GenerateOtpCode();
                Console.WriteLine(Code);

                //get the user by his email
                clsUser user = await clsUser.GetUserByEmailAsync(LoginCredentials.Email);
                if(user.IsEmailVerified)
                {
                    //set all the existing otps for the current user as used
                    await clsOtp.SetAllOtpUsedAsync(user.UserId);

                    // new otp record
                    clsOtp Otp = new clsOtp(new OtpDto(Guid.NewGuid().ToString(), Code, DateTime.Now, DateTime.Now.AddMinutes(5), user.UserId, false));

                    //if the otp was added successfully then send an email to the user with the otp code
                    if (await Otp.AddNewOtp())
                    {

                        _emailService.SendEmail(new message(new List<string>() { LoginCredentials.Email }, "your login verification code", Code.ToString()));
                        return Ok(new {IsFound=true , IsEmailVerified = true});
                    }
                    else
                        return Ok(new { IsFound = false, IsEmailVerified = false });
                }
                else
                {
                    return Ok(new { IsFound = true, IsEmailVerified = false });
                }


            }
            return Ok(new { IsFound = false, IsEmailVerified = false });
        }

        [HttpPost("2falogin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> TwoFaLogin([FromBody] int? Code)
        {
            if (Code == null || Code <= 0)
                return BadRequest("invalid data");

            int RefreshTokenValidityTime = Convert.ToInt32(_configuration["JWT:RefreshTokenValidityTime"]);


            clsUser? User = await clsUser.TwoFaLoginAsync(Code);

            string RefreshToken = TokensProvider.GenerateRefreshToken();

            UserResponseDto? Response = null;

            if (User != null)
            {
                //delete all refresh tokens that belongs to this user
                await clsRefreshToken.DeleteAllRefreshTokensByUserIdAsync(User.UserId);

                //add new refresh token record for this user
                clsRefreshToken Rtoken = new clsRefreshToken(new RefreshTokenDto(Guid.NewGuid().ToString(), RefreshToken, DateTime.Now,
                    DateTime.Now.AddMinutes(RefreshTokenValidityTime), User.UserId));

                if (await Rtoken.AddNewRefreshTokenAsync())
                {
                    Response = _mapper.Map<UserResponseDto>(User);
                    Response.RefreshToken = RefreshToken;
                    Response.Token = TokensProvider.GenerateLogInAuthenticationtoken(_configuration, User);

                    //set all the otp codes belongs to this user as used 
                    await clsOtp.SetAllOtpUsedAsync(User.UserId);

                    return Ok(Response);

                }




                return Ok(null);
            }

            return Ok(null);
        }

        [HttpPost("forgot-password")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ForgotPasswordAsync(string Email)
        {
            if (Email == null || string.IsNullOrEmpty(Email) || string.IsNullOrWhiteSpace(Email))
            {
                return BadRequest("invalid data");
            }

            clsUser? User = await clsUser.GetUserByEmailAsync(Email);

            if (User == null)
            {
                return Ok(false);
            }

            string TokenId = Guid.NewGuid().ToString();
            string Token = Guid.NewGuid().ToString();

            clsChangePasswordToken CpToken = new clsChangePasswordToken(new ChangePassWordTokenDto(
                TokenId, Token, DateTime.Now, DateTime.Now.AddMinutes(10), User.UserId));

            if (await CpToken.AddNewChangePassWordTokenAsync())
            {
                string RestPassWordUrl = $"http://localhost:5173/reset-password?token={Token}&useremail={User.Email}"; //build the reset password page + the token

                _emailService.
                    SendEmail(new message(new List<string> { Email }, "your change password link", RestPassWordUrl));

                return Ok(true);
            }

            return Ok(false);

        }

        [HttpPut("reset-password")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ResetPasswordAsync(ResetPasswordModel Model)
        {
            if (Model == null || string.IsNullOrEmpty(Model.Token) || string.IsNullOrWhiteSpace(Model.Token) ||
                string.IsNullOrEmpty(Model.Email) || string.IsNullOrWhiteSpace(Model.Email) || string.IsNullOrEmpty(Model.NewPassword) ||
                string.IsNullOrWhiteSpace(Model.NewPassword))
            {
                return BadRequest("invalid data");
            }

            clsUser? User = await clsUser.GetUserByEmailAsync(Model.Email);

            if (User != null)
            {
                clsChangePasswordToken? token = await clsChangePasswordToken.GetValidTokenAsync(Model.Token);

                if (token != null)
                {
                    User.PassWord = Model.NewPassword;

                    if (await User.ResetPassWordAsync())
                    {
                        return Ok(true);
                    }
                    //this return value will be changed into an anonymous object to contain the status and the message
                    return Ok(false);
                }
            }
            //this return value will be changed into an anonymous object to contain the status and the message
            return Ok(false);
        }


        [HttpPost("refresh-token")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RefreshToken(RefreshTokenModel Model)
        {
            
            if (Model == null || string.IsNullOrEmpty(Model.UserId) || string.IsNullOrWhiteSpace(Model.UserId) ||
                string.IsNullOrEmpty(Model.RefreshToken)||string.IsNullOrWhiteSpace(Model.RefreshToken))
            {
                return BadRequest("invalid data");
            }

            int RefreshTokenValidityTime =Convert.ToInt32(_configuration["JWT:RefreshTokenValidityTime"]);

            clsUser? User = await clsUser.GetUserByIdAsync(Model.UserId);

            if (User != null)
            {
                clsRefreshToken? CurrentRefreshToken = await clsRefreshToken.GetValidRefreshTokenAsync(Model.RefreshToken);

                if (CurrentRefreshToken != null) 
                {
                    //delete the old refresh token for the curent user
                    await clsRefreshToken.DeleteAllRefreshTokensByUserIdAsync(Model.UserId);

                    string ReftokenValue = Guid.NewGuid().ToString();
                    string NewAccessToken = "";

                    //create a new refresh token
                    clsRefreshToken NewRefreshToken = new clsRefreshToken(new RefreshTokenDto(Guid.NewGuid().ToString(),
                      ReftokenValue ,DateTime.Now,DateTime.Now.AddMinutes(RefreshTokenValidityTime),User.UserId));

                    if (await NewRefreshToken.AddNewRefreshTokenAsync())
                    {
                        NewAccessToken = TokensProvider.GenerateLogInAuthenticationtoken(_configuration, User);
                        return Ok(new { RefreshToken = ReftokenValue, Jwt = NewAccessToken });
                    }
                    else
                        return Ok(null );
                }
            }

            return Ok(null);
        }
       
        [HttpGet("isExists-with-email")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> IsUserExistsByEmailAsync([FromQuery]string Email ,[FromQuery] string? UserId)
        {
            if(Email== null||string.IsNullOrEmpty(Email)||string.IsNullOrWhiteSpace(Email))
            {
                return BadRequest("invalid data");
            }

            bool IsExists = await clsUser.IsUserExistsByEmailAsync(Email, UserId);
            return Ok(IsExists);
        }


        [HttpGet("isExists-with-username")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> IsUserExistsByUserNameAsync([FromQuery] string UserName,[FromQuery] string? UserId)
        {
            if (UserName == null || string.IsNullOrEmpty(UserName) || string.IsNullOrWhiteSpace(UserName))
            {
                return BadRequest("invalid data");
            }

            bool IsExists = await clsUser.IsUserExistsByUserNameAsync(UserName, UserId);
            return Ok(IsExists);
        }




    }
}

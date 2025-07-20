using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SocialAppApi.Models;
using SocialAppBusinessLayer;
using SocialAppDataLayer.Dtos;

namespace SocialAppApi.Controllers
{
    [Route("api/Posts")]
    [ApiController]
    [Authorize]
    [AllowAnonymous]
    
    public class PostController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        public PostController(IWebHostEnvironment env)
        {
            _env = env;
        }

        [HttpGet("post/{PostId}",Name ="GetPostById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPostByIdAsync(string PostId)
        {
            if (PostId == null || string.IsNullOrEmpty(PostId) || string.IsNullOrWhiteSpace(PostId))
                return BadRequest("invalid data");

            clsPost? Post = await clsPost.GetPostByidAsync(PostId);

            if (Post == null) 
                return NotFound($"Post with id {PostId} is not found");

            return Ok(Post.PostDto);
        }
       
        [HttpPost("addpost")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddNewPostAsync([FromForm] AddPostModel Model)
        {
            if(Model == null
                || string.IsNullOrEmpty(Model.UserId)
                || string.IsNullOrWhiteSpace(Model.UserId))
            {
                return BadRequest("invalid data");
            }

            string? MediaUrl = null;

            //handle post picture if any!
            if (Model.Media != null)
            {
                string FolderName = "Posts-Media";
                var UploadsFolder = Path.Combine(_env.WebRootPath, FolderName);

                if (!Directory.Exists(UploadsFolder))
                {
                    Directory.CreateDirectory(UploadsFolder);
                }

                var FileName = Guid.NewGuid() + Path.GetExtension(Model.Media.FileName);
                var FilePath = Path.Combine(UploadsFolder, FileName);

                using (FileStream Stream = new FileStream(FilePath, FileMode.Create))
                {
                    await Model.Media.CopyToAsync(Stream);
                }

                MediaUrl = $"{Request.Scheme}://{Request.Host}/{FolderName}/{FileName}";
                
            }

            clsPost Post = new clsPost(new PostDto(Guid.NewGuid().ToString(), Model.Content, MediaUrl,DateTime.Now,Model.UserId));

            if(await Post.AddNewPostAsync())
            {
                return CreatedAtRoute("GetPostById",new {PostId = Post.PostId}, Post.PostDto);
            }
            return StatusCode(500,"internal serve error");
        }
       
        [HttpGet("user-posts")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllPostsByUserIdAsync([FromQuery] PostsModel Model)
        {
            if (Model == null || string.IsNullOrEmpty(Model.UserId) || string.IsNullOrWhiteSpace(Model.UserId) || string.IsNullOrEmpty(Model.CurrentUserId) || string.IsNullOrWhiteSpace(Model.CurrentUserId))
                return BadRequest("invalid data");

            List<PostForCurrentUserDto> Posts = await clsPost.GetAllPostsByUserIdAsync(Model.UserId , Model.CurrentUserId);

            return Ok(Posts);
           
        }
        
        [HttpGet("user-friends-posts/{UserId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllPostForCurrentUserAndFriendsAsync(string UserId)
        {
            if(UserId == null || string.IsNullOrEmpty(UserId)|| string.IsNullOrWhiteSpace(UserId))
            {
                return BadRequest("invalid data");
            }

            List<PostWithUserAndDetailsDto> Posts = await clsPost.GetAllPostForCurrentUserAndFriendsAsync(UserId);

            return Ok(Posts);
        }

    }
}

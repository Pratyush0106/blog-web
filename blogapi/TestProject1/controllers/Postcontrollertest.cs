//using BlogApi.Controllers;
//using BlogApi.Data;
//using BlogApi.Model;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
//using Moq;
//using NUnit.Framework;
//using System.Collections.Generic;
//using System.Linq;
//using System.Security.Claims;
//using System.Threading.Tasks;

//namespace BlogApi.Tests.Controllers
//{
//    [TestFixture]
//    public class PostsControllerTests
//    {
//        private AppDbContext _context;
//        private PostsController _controller;
//        private Mock<IConfiguration> _mockConfiguration;

//        [SetUp]
//        public void SetUp()
//        {
//            var options = new DbContextOptionsBuilder<AppDbContext>()
//                .UseInMemoryDatabase(databaseName: "BlogApiTest")
//                .Options;

//            _mockConfiguration = new Mock<IConfiguration>();
//            _context = new AppDbContext(options, _mockConfiguration.Object);
//            _controller = new PostsController(_context);

//            SeedDatabase();
//        }

//        [TearDown]
//        public void TearDown()
//        {
//            _context.Database.EnsureDeleted();
//            _context.Dispose();
//        }

//        private void SeedDatabase()
//        {
//            var user = new User
//            {
//                Id = 1,
//                Username = "testuser",
//                Password = "hashedpassword",
//                Email = "testuser@example.com",
//                FullName = "Test User"
//            };

//            var posts = new List<Post>
//            {
//                new Post { Id = 1, Title = "Post 1", Content = "Content 1", Genre = "Genre 1", Image = "Image1.jpg", AuthorId = 1, Author = user },
//                new Post { Id = 2, Title = "Post 2", Content = "Content 2", Genre = "Genre 2", Image = "Image2.jpg", AuthorId = 1, Author = user }
//            };

//            _context.Users.Add(user);
//            _context.Posts.AddRange(posts);
//            _context.SaveChanges();
//        }

//        [Test]
//        public async Task GetPosts_ReturnsAllPosts()
//        {
//            // Act
//            var result = await _controller.GetPosts();

//            // Assert
//            Assert.That(result, Is.TypeOf<ActionResult<IEnumerable<PostDTO>>>());
//            var actionResult = result as ActionResult<IEnumerable<PostDTO>>;
//            Assert.That(actionResult.Value, Is.TypeOf<List<PostDTO>>());
//            var returnValue = actionResult.Value as List<PostDTO>;
//            Assert.That(returnValue.Count, Is.EqualTo(2));
//        }

//        [Test]
//        public async Task GetPost_ReturnsPost_WhenPostExists()
//        {
//            // Arrange
//            var postId = 1;

//            // Act
//            var result = await _controller.GetPost(postId);

//            // Assert
//            Assert.That(result, Is.TypeOf<ActionResult<PostDTO>>());
//            var actionResult = result as ActionResult<PostDTO>;
//            Assert.That(actionResult.Value, Is.TypeOf<PostDTO>());
//            var returnValue = actionResult.Value as PostDTO;
//            Assert.That(returnValue.Id, Is.EqualTo(postId));
//        }

//        [Test]
//        public async Task GetPost_ReturnsNotFound_WhenPostDoesNotExist()
//        {
//            // Arrange
//            var postId = 3;

//            // Act
//            var result = await _controller.GetPost(postId);

//            // Assert
//            Assert.That(result.Result, Is.TypeOf<NotFoundResult>());
//        }

//        [Test]
//        public async Task CreatePost_ReturnsCreatedPost()
//        {
//            // Arrange
//            var postDto = new PostDTO { Title = "New Post", Content = "New Content", Genre = "New Genre", Image = "NewImage.jpg" };
//            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
//            {
//                new Claim(ClaimTypes.NameIdentifier, "testuser")
//            }, "mock"));

//            _controller.ControllerContext = new ControllerContext
//            {
//                HttpContext = new DefaultHttpContext { User = user }
//            };

//            // Act
//            var result = await _controller.CreatePost(postDto);

//            // Assert
//            Assert.That(result, Is.TypeOf<ActionResult<PostDTO>>());
//            var actionResult = result as ActionResult<PostDTO>;
//            Assert.That(actionResult.Result, Is.TypeOf<CreatedAtActionResult>());
//            var createdAtActionResult = actionResult.Result as CreatedAtActionResult;
//            Assert.That(createdAtActionResult.Value, Is.TypeOf<PostDTO>());
//            var returnValue = createdAtActionResult.Value as PostDTO;
//            Assert.That(returnValue.Title, Is.EqualTo(postDto.Title));
//        }

//        [Test]
//        public async Task UpdatePost_ReturnsNoContent_WhenPostIsUpdated()
//        {
//            // Arrange
//            var postId = 1;
//            var postDto = new PostDTO { Id = postId, Title = "Updated Post", Content = "Updated Content", Genre = "Updated Genre", Image = "UpdatedImage.jpg" };

//            // Act
//            var result = await _controller.UpdatePost(postId, postDto);

//            // Assert
//            Assert.That(result, Is.TypeOf<NoContentResult>());
//        }

//        [Test]
//        public async Task DeletePost_ReturnsNoContent_WhenPostIsDeleted()
//        {
//            // Arrange
//            var postId = 1;

//            // Act
//            var result = await _controller.DeletePost(postId);

//            // Assert
//            Assert.That(result, Is.TypeOf<NoContentResult>());
//        }
//    }
//}
using BlogApi.Data;
using BlogApi.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;

[ApiController]
[Route("api/[controller]")]
public class PostsController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _environment;

    public PostsController(AppDbContext context, IWebHostEnvironment environment)
    {
        _context = context;
        _environment = environment;
    }

    [HttpGet]
    [Authorize(Policy = "UserOrAdmin")]
    public async Task<ActionResult<IEnumerable<PostDTO>>> GetPosts()
    {
        return await _context.Posts
            .Select(p => new PostDTO
            {
                Id = p.Id,
                Title = p.Title,
                Content = p.Content,
                Genre = p.Genre,
                ImageUrl = p.ImageUrl // Set ImageUrl correctly
            })
            .ToListAsync();
    }

    [HttpGet("{id}")]
    [Authorize(Policy = "UserOrAdmin")]
    public async Task<ActionResult<PostDTO>> GetPost(int id)
    {
        var post = await _context.Posts
            .Select(p => new PostDTO
            {
                Id = p.Id,
                Title = p.Title,
                Content = p.Content,
                Genre = p.Genre,
                ImageUrl = p.ImageUrl // Set ImageUrl correctly
            })
            .FirstOrDefaultAsync(p => p.Id == id);

        if (post == null)
        {
            return NotFound();
        }

        return Ok(post);
    }

    [HttpGet("ByUser/{userId}")]
    [Authorize(Policy = "UserOrAdmin")]
    public async Task<ActionResult<IEnumerable<PostDTO>>> GetPostsByUser(int userId)
    {
        var posts = await _context.Posts
            .Where(p => p.AuthorId == userId)
            .Select(p => new PostDTO
            {
                Id = p.Id,
                Title = p.Title,
                Content = p.Content,
                Genre = p.Genre,
                ImageUrl = p.ImageUrl // Set ImageUrl correctly
            })
            .ToListAsync();

        if (posts == null || !posts.Any())
        {
            return NotFound();
        }

        return Ok(posts);
    }

    [HttpPost]
    [Authorize(Policy = "UserOrAdmin")]
    public async Task<ActionResult<PostDTO>> CreatePost([FromForm] PostDTO postDto)
    {
        var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);

        if (user == null)
        {
            return Unauthorized();
        }

        var post = new Post
        {
            Title = postDto.Title,
            Content = postDto.Content,
            Genre = postDto.Genre,
            AuthorId = user.Id,
            ImageUrl = await SaveImage(postDto.ImageUrl)
        };

        _context.Posts.Add(post);
        await _context.SaveChangesAsync();

        var postToReturn = new PostDTO
        {
            Id = post.Id,
            Title = post.Title,
            Content = post.Content,
            Genre = post.Genre,
            ImageUrl = post.ImageUrl // Set ImageUrl correctly
        };

        return CreatedAtAction(nameof(GetPost), new { id = post.Id }, postToReturn);
    }

    private async Task<string> SaveImage(string imageUrl)
    {
        throw new NotImplementedException();
    }

    [HttpPut("{id}")]
    [Authorize(Policy = "UserOrAdmin")]
    public async Task<IActionResult> UpdatePost(int id, [FromForm] PostDTO postDto)
    {
        if (id != postDto.Id)
        {
            return BadRequest();
        }

        var post = await _context.Posts.FindAsync(id);
        if (post == null)
        {
            return NotFound();
        }

        if (!string.IsNullOrEmpty(postDto.Title))
        {
            post.Title = postDto.Title;
        }

        if (!string.IsNullOrEmpty(postDto.Content))
        {
            post.Content = postDto.Content;
        }

        if (!string.IsNullOrEmpty(postDto.Genre))
        {
            post.Genre = postDto.Genre;
        }

        if (postDto.ImageUrl != null)
        {
            post.ImageUrl = await SaveImage(postDto.ImageUrl);
        }

        _context.Entry(post).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "AdminOnly")]
    public async Task<IActionResult> DeletePost(int id)
    {
        var post = await _context.Posts.FindAsync(id);

        if (post == null)
        {
            return NotFound();
        }

        _context.Posts.Remove(post);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private async Task<string> SaveImage(IFormFile image)
    {
        var directoryPath = Path.Combine(_environment.WebRootPath, "Images");
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        var filePath = Path.Combine(directoryPath, image.FileName);
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await image.CopyToAsync(stream);
        }
        return $"/Images/{image.FileName}";
    }
}

using BlogApi.Data;
using BlogApi.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Collections.Generic;

[ApiController]
[Route("api/[controller]")]
public class PostsController : ControllerBase
{
    private readonly AppDbContext _context;

    public PostsController(AppDbContext context)
    {
        _context = context;
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
                ImageUrl = p.ImageUrl // Use ImageUrl instead of Image
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
                ImageUrl = p.ImageUrl // Use ImageUrl instead of Image
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
                ImageUrl = p.ImageUrl // Use ImageUrl instead of Image
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
            ImageUrl = postDto.ImageUrl // Use ImageUrl directly
        };

        _context.Posts.Add(post);
        await _context.SaveChangesAsync();

        var postToReturn = new PostDTO
        {
            Id = post.Id,
            Title = post.Title,
            Content = post.Content,
            Genre = post.Genre,
            ImageUrl = post.ImageUrl // Use ImageUrl instead of Image
        };

        return CreatedAtAction(nameof(GetPost), new { id = post.Id }, postToReturn);
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

        if (!string.IsNullOrEmpty(postDto.ImageUrl))
        {
            post.ImageUrl = postDto.ImageUrl; // Use ImageUrl directly
        }

        _context.Entry(post).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "UserOrAdmin")]
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
}
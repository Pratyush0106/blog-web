////using blogapi.Model;
////using BlogApi.Data;
////using BlogApi.Model;
////using Microsoft.AspNetCore.Authorization;
////using Microsoft.AspNetCore.Mvc;
////using Microsoft.EntityFrameworkCore;

////[ApiController]
////[Route("api/[controller]")]
////public class CommentsController : ControllerBase
////{
////    private readonly AppDbContext _context;

////    public CommentsController(AppDbContext context)
////    {
////        _context = context;
////    }

////    [HttpGet]
////    //[Authorize(Policy = "UserOrAdmin")]
////    public async Task<ActionResult<IEnumerable<CommentsDTO>>> GetComments()
////    {
////        return await _context.Comments
////            .Select(c => new CommentsDTO
////            {
////                Id = c.Id,
////                Content = c.Content,
////                PostId = c.PostId,
////                UserId = c.UserId
////            })
////            .ToListAsync();
////    }

////    [HttpGet("{id}")]
////    [Authorize(Policy = "UserOrAdmin")]
////    public async Task<ActionResult<CommentsDTO>> GetComment(int id)
////    {
////        var comment = await _context.Comments
////            .Select(c => new CommentsDTO
////            {
////                Id = c.Id,
////                Content = c.Content,
////                PostId = c.PostId,
////                UserId = c.UserId
////            })
////            .FirstOrDefaultAsync(c => c.Id == id);

////        if (comment == null)
////        {
////            return NotFound();
////        }

////        return Ok(comment);
////    }

////    [HttpPost]
////    [Authorize(Policy = "UserOrAdmin")]
////    public async Task<ActionResult<CommentsDTO>> CreateComment(CommentsDTO commentDto)
////    {
////        var comment = new Comment
////        {
////            Content = commentDto.Content,
////            PostId = commentDto.PostId,
////            UserId = commentDto.UserId,
////        };

////        _context.Comments.Add(comment);
////        await _context.SaveChangesAsync();

////        commentDto.Id = comment.Id;

////        return CreatedAtAction(nameof(GetComment), new { id = comment.Id }, commentDto);
////    }

////    [HttpPut("{id}")]
////    [Authorize(Policy = "UserOrAdmin")]
////    public async Task<IActionResult> UpdateComment(int id, CommentsDTO commentDto)
////    {
////        if (id != commentDto.Id)
////        {
////            return BadRequest();
////        }

////        var comment = await _context.Comments.FindAsync(id);
////        if (comment == null)
////        {
////            return NotFound();
////        }

////        comment.Content = commentDto.Content;
////        comment.PostId = commentDto.PostId;
////        comment.UserId = commentDto.UserId;

////        _context.Entry(comment).State = EntityState.Modified;
////        await _context.SaveChangesAsync();

////        return NoContent();
////    }

////    [HttpDelete("{id}")]
////    [Authorize(Policy = "AdminOnly")]
////    public async Task<IActionResult> DeleteComment(int id)
////    {
////        var comment = await _context.Comments.FindAsync(id);

////        if (comment == null)
////        {
////            return NotFound();
////        }

////        _context.Comments.Remove(comment);
////        await _context.SaveChangesAsync();

////        return NoContent();
////    }
////}




//using BlogApi.Data;
//using blogapi.Model;
//using BlogApi.Model;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using System.Security.Claims;

//[ApiController]
//[Route("api/[controller]")]
//public class CommentsController : ControllerBase
//{
//    private readonly AppDbContext _context;

//    public CommentsController(AppDbContext context)
//    {
//        _context = context;
//    }

//    [HttpGet]
//    [Authorize(Policy = "UserOrAdmin")]
//    public async Task<ActionResult<IEnumerable<CommentsDTO>>> GetComments()
//    {
//        return await _context.Comments
//            .Select(c => new CommentsDTO
//            {
//                Id = c.Id,
//                Content = c.Content,
//                PostId = c.PostId,
//                UserId = c.UserId

//            })
//            .ToListAsync();
//    }

//    [HttpGet("{id}")]
//    [Authorize(Policy = "UserOrAdmin")]
//    public async Task<ActionResult<CommentsDTO>> GetComment(int id)
//    {
//        var comment = await _context.Comments
//            .Select(c => new CommentsDTO
//            {
//                Id = c.Id,
//                Content = c.Content,
//                PostId = c.PostId,
//                UserId = c.UserId,

//            })
//            .FirstOrDefaultAsync(c => c.Id == id);

//        if (comment == null)
//        {
//            return NotFound();
//        }

//        return Ok(comment);
//    }

//    [HttpPost]
//    [Authorize(Policy = "UserOrAdmin")]
//    public async Task<ActionResult<CommentsDTO>> CreateComment(CommentsDTO commentDto)
//    {
//        var comment = new Comment
//        {
//            Content = commentDto.Content,
//            PostId = commentDto.PostId,
//            UserId = commentDto.UserId,
//        };

//        _context.Comments.Add(comment);
//        await _context.SaveChangesAsync();

//        commentDto.Id = comment.Id;

//        return CreatedAtAction(nameof(GetComment), new { id = comment.Id }, commentDto);
//    }
//    //[HttpPost]
//    //[Authorize(Policy = "UserOrAdmin")]
//    //public async Task<ActionResult<CommentsDTO>> CreateComment(CommentsDTO commentDto)
//    //{
//    //    var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
//    //    if (userIdClaim == null)
//    //    {
//    //        Console.WriteLine("User ID not found in token");
//    //        return Unauthorized();
//    //    }

//    //    int userId;
//    //    if (!int.TryParse(userIdClaim, out userId))
//    //    {
//    //        Console.WriteLine("User ID is not in the correct format");
//    //        return BadRequest("Invalid User ID format");
//    //    }

//    //    var comment = new Comment
//    //    {
//    //        Content = commentDto.Content,
//    //        PostId = commentDto.PostId,
//    //        UserId = userId,
//    //        //Username = commentDto.Username
//    //    };

//    //    _context.Comments.Add(comment);
//    //    await _context.SaveChangesAsync();

//    //    commentDto.Id = comment.Id;

//    //    return CreatedAtAction(nameof(GetComment), new { id = comment.Id }, commentDto);
//    //}

//    [HttpPut("{id}")]
//    [Authorize(Policy = "UserOrAdmin")]
//    public async Task<IActionResult> UpdateComment(int id, CommentsDTO commentDto)
//    {
//        if (id != commentDto.Id)
//        {
//            return BadRequest();
//        }

//        var comment = await _context.Comments.FindAsync(id);
//        if (comment == null)
//        {
//            return NotFound();
//        }

//        comment.Content = commentDto.Content;
//        comment.PostId = commentDto.PostId;
//        comment.UserId = commentDto.UserId;

//        _context.Entry(comment).State = EntityState.Modified;
//        await _context.SaveChangesAsync();

//        return NoContent();
//    }

//    [HttpDelete("{id}")]
//    [Authorize(Policy = "UserOrAdmin")]
//    public async Task<IActionResult> DeleteComment(int id)
//    {
//        var comment = await _context.Comments.FindAsync(id);

//        if (comment == null)
//        {
//            return NotFound();
//        }

//        _context.Comments.Remove(comment);
//        await _context.SaveChangesAsync();

//        return NoContent();
//    }
//}
using BlogApi.Data;
using blogapi.Model;
using BlogApi.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

[ApiController]
[Route("api/[controller]")]
public class CommentsController : ControllerBase
{
    private readonly AppDbContext _context;

    public CommentsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    [Authorize(Policy = "UserOrAdmin")]
    public async Task<ActionResult<IEnumerable<CommentsDTO>>> GetComments()
    {
        return await _context.Comments
            .Select(c => new CommentsDTO
            {
                Id = c.Id,
                Content = c.Content,
                PostId = c.PostId,
                UserId = c.UserId,
                Username = c.Username
            })
            .ToListAsync();
    }

    [HttpGet("{id}")]
    [Authorize(Policy = "UserOrAdmin")]
    public async Task<ActionResult<CommentsDTO>> GetComment(int id)
    {
        var comment = await _context.Comments
            .Select(c => new CommentsDTO
            {
                Id = c.Id,
                Content = c.Content,
                PostId = c.PostId,
                UserId = c.UserId,
                Username = c.Username
            })
            .FirstOrDefaultAsync(c => c.Id == id);

        if (comment == null)
        {
            return NotFound();
        }

        return Ok(comment);
    }

    [HttpGet("post/{postId}")]
    [Authorize(Policy = "UserOrAdmin")]
    public async Task<ActionResult<IEnumerable<CommentsDTO>>> GetCommentsByPostId(int postId)
    {
        var comments = await _context.Comments
            .Where(c => c.PostId == postId)
            .Select(c => new CommentsDTO
            {
                Id = c.Id,
                Content = c.Content,
                PostId = c.PostId,
                UserId = c.UserId,
                Username = c.Username
            })
            .ToListAsync();

        if (comments == null || comments.Count == 0)
        {
            return NotFound();
        }

        return Ok(comments);
    }

    [HttpPost]
    [Authorize(Policy = "UserOrAdmin")]
    public async Task<ActionResult<CommentsDTO>> CreateComment(CommentsDTO commentDto)
    {
        var comment = new Comment
        {
            Content = commentDto.Content,
            PostId = commentDto.PostId,
            UserId = commentDto.UserId,
            Username = commentDto.Username
        };

        _context.Comments.Add(comment);
        await _context.SaveChangesAsync();

        commentDto.Id = comment.Id;

        return CreatedAtAction(nameof(GetComment), new { id = comment.Id }, commentDto);
    }

    [HttpPut("{id}")]
    [Authorize(Policy = "UserOrAdmin")]
    public async Task<IActionResult> UpdateComment(int id, CommentsDTO commentDto)
    {
        if (id != commentDto.Id)
        {
            return BadRequest();
        }

        var comment = await _context.Comments.FindAsync(id);
        if (comment == null)
        {
            return NotFound();
        }

        comment.Content = commentDto.Content;
        comment.PostId = commentDto.PostId;
        comment.UserId = commentDto.UserId;
        comment.Username = commentDto.Username;

        _context.Entry(comment).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "UserOrAdmin")]
    public async Task<IActionResult> DeleteComment(int id)
    {
        var comment = await _context.Comments.FindAsync(id);

        if (comment == null)
        {
            return NotFound();
        }

        _context.Comments.Remove(comment);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}

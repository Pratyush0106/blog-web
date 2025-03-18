using BlogApi.Model;
using System.ComponentModel.DataAnnotations;

namespace blogapi.Model
{
    public class CommentsDTO
    {
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public int PostId { get; set; }

        [Required]
        public int UserId { get; set; }
        
        public string  Username { get; set; }
    }
}


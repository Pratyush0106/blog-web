using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BlogApi.Model
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        [ForeignKey("Post")]
        public int PostId { get; set; }

        public Post Post { get; set; }

        [Required]

        [ForeignKey("User")]
        public int UserId { get; set; }
        public string Username { get; set; }

        public User User { get; set; }
       
    }
}

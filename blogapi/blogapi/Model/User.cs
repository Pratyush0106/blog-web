using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BlogApi.Model
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public DateTime CreationDate { get; set; } = DateTime.UtcNow;

        [JsonIgnore]
        public ICollection<Post> Posts { get; set; } = new List<Post>();

        [JsonIgnore]
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}

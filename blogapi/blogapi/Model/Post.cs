using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BlogApi.Model
{
    public class Post
    {
        [Key]
        public int Id { get; set; }

        //[Required]
        public string Title { get; set; }

        //[Required]
        public string Content { get; set; }

        //[Required]
        public string Genre { get; set; }

        //[Required]
        [ForeignKey("AuthorId")]
        public int AuthorId { get; set; }

       
        [JsonIgnore]
        public User Author { get; set; } 


        public DateTime  PublishedAt { get; set; }

     

        //[Required]
        public string ImageUrl { get; set; }

        [JsonIgnore]
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}

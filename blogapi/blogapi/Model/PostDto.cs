using System.ComponentModel.DataAnnotations;

namespace BlogApi.Model
{
    public class PostDTO
    {
        public int Id { get; set; }
       
        public string Title { get; set; }
       
        public string Content { get; set; }
       
        public string Genre { get; set; }
  
        public string ImageUrl { get; set; }

      
    }
}

using System.ComponentModel.DataAnnotations;

namespace BlogApi.Model
{
    public class LoginRequest
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
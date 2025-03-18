using System.ComponentModel.DataAnnotations;

namespace blogapi.Model
{
    public class ResetPasswordRequestDto
    {

        
            [Required]
           
            public string Username { get; set; }

            [Required]
            public string Token { get; set; }

            [Required]
            [MinLength(6)]
            public string Password { get; set; }
        }
    }


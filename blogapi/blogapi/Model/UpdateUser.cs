using System.ComponentModel.DataAnnotations;

namespace blogapi.Model
{
    public class UpdateUser
    {
        
        public string FullName { get; set; }    

        public string Email { get; set; }
       
        public string Password { get; set; }
    }
}

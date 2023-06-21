using System.ComponentModel.DataAnnotations;

namespace DatingApp.Dtos
{
    public class RegisterDto
    {

        [Required]
        public string userName { get; set; }
        [Required]
        [StringLength(8,MinimumLength =4,ErrorMessage ="String is 8 Max and 4 Min")]
        public string Password { get; set; }
    }
}

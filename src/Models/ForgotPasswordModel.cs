using System.ComponentModel.DataAnnotations;

namespace TheMuscleBar.Models
{
    public class ForgotPasswordModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using TheMuscleBar.AppCode.Enums;

namespace TheMuscleBar.Models
{
    public class Register : Response
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please select role")]
        public Role Role { get; set; }

        [Required(ErrorMessage = "Please enter name")]
        [StringLength(200)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter Mobile ")]
        [StringLength(10)]

        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        public string Address { get; set; }
        public string PinCode { get; set; }
    }
    public class RegisterViewModel: Register
    {
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and compare password do not match..")]
        public string ConfirmPassword { get; set; }
    }
}

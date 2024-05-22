
using System.ComponentModel.DataAnnotations;

namespace WebApplication14.ViewModels.Account
{
    public class RegisterVm
    {
        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string Surname { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        [DataType(DataType.Password) , Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}

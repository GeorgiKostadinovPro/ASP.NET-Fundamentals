using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models.Users
{
    public class RegisterViewModel
    {
        [System.ComponentModel.DataAnnotations.Required]
        [StringLength(20, MinimumLength = 5)]
        public string UserName { get; set; } = null!;

        [System.ComponentModel.DataAnnotations.Required]
        [StringLength(60, MinimumLength = 10)]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [System.ComponentModel.DataAnnotations.Required]
        [StringLength(20, MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [System.ComponentModel.DataAnnotations.Required]
        [Compare(nameof(Password))]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } = null!;
    }
}

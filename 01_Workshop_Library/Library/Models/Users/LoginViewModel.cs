using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace Library.Models.Users
{
    public class LoginViewModel
    {
        [System.ComponentModel.DataAnnotations.Required]
        public string UserName { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        //[UIHint("hidden")]
        //public string ReturnUrl { get; set; }
    }
}

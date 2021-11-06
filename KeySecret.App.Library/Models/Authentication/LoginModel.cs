using System;
using System.ComponentModel.DataAnnotations;

namespace KeySecret.App.Library.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters")]
        public string Password { get; set; }

        public string Token { get; set; }

        public DateTime Expires { get; set; }
    }
}
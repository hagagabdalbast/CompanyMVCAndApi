using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BL.Models
{
    public class ResetPasswordVM
    {
        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "min length 6")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "min length 6")]
        [Compare("Password", ErrorMessage = "password not matched")]
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}

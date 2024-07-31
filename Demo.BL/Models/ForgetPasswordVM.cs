using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BL.Models
{
    public class ForgetPasswordVM
    {
        [EmailAddress(ErrorMessage = "invalid mail")]
        [Required(ErrorMessage = "the Email Address is required")]
        public string Email { get; set; }
    }
}

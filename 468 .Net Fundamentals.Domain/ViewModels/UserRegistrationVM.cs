using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace _468_.Net_Fundamentals.Domain.ViewModels
{
    public class UserRegistrationVM
    {
        [Required(ErrorMessage = "Name is required.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}

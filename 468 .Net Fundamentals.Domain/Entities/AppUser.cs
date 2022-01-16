using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace _468_.Net_Fundamentals.Domain.Entities
{
    public class AppUser : IdentityUser
    {
        public string ImagePath { get; set; }
    }
}

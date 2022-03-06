﻿using _468_.Net_Fundamentals.Domain.EnumType;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace _468_.Net_Fundamentals.Domain.Entities
{
    public class AppUser : IdentityUser
    {
    
        public string ImagePath { get; set; }

        /*[NotMapped]
        public string Token { get; set; }*/

        public virtual ICollection<Project> Projects { get; set; }
    }
}

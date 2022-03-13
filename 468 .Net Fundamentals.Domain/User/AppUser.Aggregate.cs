using _468_.Net_Fundamentals.Domain.EnumType;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace _468_.Net_Fundamentals.Domain.Entities
{
    public partial class AppUser 
    {
        public void AddProject(string name)
        {
            this.Projects.Add(new Project(this.Id, name));
        }
    }
}

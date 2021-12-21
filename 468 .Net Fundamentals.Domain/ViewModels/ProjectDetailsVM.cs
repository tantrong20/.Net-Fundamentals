using _468_.Net_Fundamentals.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace _468_.Net_Fundamentals.Domain.ViewModels
{
    public class ProjectDetailsVM
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int CreatedBy { get; set; }

        public IList<BusinessVM> Businesses { get; set; }
    }
} 

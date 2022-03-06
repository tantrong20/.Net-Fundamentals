using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _468_.Net_Fundamentals.Domain.Entities
{
    [Table("Project")]
    public class Project : EntityBase<int>
    {
        private Project()
        {
            Businesses = new HashSet<Business>();
        }
        public Project(string name, string userId)
        {
            Name = name;
            CreatedBy = userId;
            CreatedOn = DateTime.Now;
        }

        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Created On")]
        public DateTime CreatedOn { get; set; }

        [Required]
        public string CreatedBy { get; set; }

        [ForeignKey("CreatedBy")]
        public virtual AppUser User { get; set; }

        public virtual ICollection<Business> Businesses { get; private set; }

        public void AddBusiness(string name)
        {
            Businesses.Add(new Business(this.Id, name));
        }
    }
}



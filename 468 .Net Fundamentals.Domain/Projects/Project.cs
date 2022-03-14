using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _468_.Net_Fundamentals.Domain.Entities
{
    [Table("Project")]
    public partial class Project : EntityBase<int>
    {
        public Project()
        {
            Businesses = new HashSet<Business>();
            Tags = new HashSet<Tag>();
        }
        // From feture branch
        // Test conflict
        // Create conflict
        /*[Required]
        public string Name { get; private set; }

        [Required]
        [Display(Name = "Created On")]
        public DateTime CreatedOn { get; private set; }

        [Required]
        public string CreatedBy { get; private set ; }

        [ForeignKey("CreatedBy")]
        public virtual AppUser User { get; private set; }

        public virtual ICollection<Business> Businesses { get; private set; }

        public virtual ICollection<Tag> Tags { get; private set; }*/

    }
}



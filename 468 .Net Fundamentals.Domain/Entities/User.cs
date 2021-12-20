using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace _468_.Net_Fundamentals.Domain.Entities
{
    [Table("User")]
    public class User : EntityBase<int>
    {
        [StringLength(50)]
        public string Name { get; set; }

        public string Email { get; set; }

        [Required]
        public int Role { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace _468_.Net_Fundamentals.Domain.Entities
{
    [Table("ProjectMember")]
    public class ProjectMember 
    {
        public int ProjectId { get; set; }

        public int MemberId { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Add On")]
        public DateTime AddOn{ get; set; }



        public virtual User User { get; set; }

        public virtual Project Project { get; set; }
    }
}

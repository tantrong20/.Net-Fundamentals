using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace _468_.Net_Fundamentals.Domain.Entities
{
    [Table("Card")]
    public class Card : EntityBase<int>
    {
        public string Name { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreatedOn { get; set; }

        public string Description { get; set; }

        public int? Priority { get; set; }

        public int BusinessId { get; set; }


        [ForeignKey("BusinessId")]
        public virtual Business Business { get; set; }

        public virtual IList<Todo> Todos { get; set; }
        // Status = List/Stage: Backlog, inprocess, done
    }
}

using _468_.Net_Fundamentals.Domain.EnumType;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace _468_.Net_Fundamentals.Domain.Entities
{
    [Table("Card")]
    public partial class Card
    {
        public Card()
        {
            Todos = new HashSet<Todo>();
            CardTags = new HashSet<CardTag>();
            CardAssigns = new HashSet<CardAssign>();

        }
        public string Name { get; private set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CreatedOn { get; private set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? Duedate { get; private set; }

        public string? Description { get; private set; }

        public TaskPriority Priority { get; private set; }

        public float Index { get; private set; }

        public int BusinessId { get; private set; }

        [ForeignKey("BusinessId")]
        public virtual Business Business { get; private set; }

        public virtual ICollection<Todo> Todos { get; private set; }
        public virtual ICollection<CardAssign> CardAssigns { get; private set; }
        public virtual ICollection<CardTag> CardTags { get; private set; }
    }
}

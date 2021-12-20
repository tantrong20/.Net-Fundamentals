using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace _468_.Net_Fundamentals.Domain.Entities
{
    [Table("Tag")]
    public class Tag : EntityBase<int>
    {
        public string Name { get; set; }

        public string Color { get; set; }

        public virtual IList<CardTag> CardTags { get; set; }
    }
}

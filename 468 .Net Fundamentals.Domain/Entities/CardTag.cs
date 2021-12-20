using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace _468_.Net_Fundamentals.Domain.Entities
{
    [Table("CardTag")]
    public class CardTag : EntityBase<int>
    {
        public int? TagId { get; set; }
        public int? CardId { get; set; }

        [ForeignKey("CardId")]
        public virtual Card Card { get; set; }

        [ForeignKey("TagId")]
        public virtual Tag Tag { get; set; }
    }
}

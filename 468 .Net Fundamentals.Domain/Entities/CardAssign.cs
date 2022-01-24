using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace _468_.Net_Fundamentals.Domain.Entities
{
    [Table("CardAssign")]
    public class CardAssign
    {

        public int CardId { get; set; }

        public string AssignTo { get; set; }

        public virtual Card Card { get; set; }

        public virtual AppUser User { get; set; }

    }
}

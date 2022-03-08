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
        public CardAssign()
        {

        }
        public CardAssign(int cardId, string userId) : this()
        {
            this.CardId = cardId;
            this.AssignTo = userId;
        }

        public int CardId { get; set; }

        public string AssignTo { get; set; }

        [ForeignKey("CardId")]
        public virtual Card Card { get; set; }

        [ForeignKey("AssignTo")]
        public virtual AppUser User { get; set; }
    }
}

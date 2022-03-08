using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace _468_.Net_Fundamentals.Domain.Entities
{
    [Table("Todo")]
    public class Todo : EntityBase<int>
    {
        public Todo()
        {

        }
        public Todo(int cardId, string name): this()
        {
            this.CardId = cardId;
            this.Name = name;
            this.IsCompleted = false;
        }
        public string Name { get; set; }
        public bool IsCompleted { get; set; }

        public int CardId { get; set; }

        [ForeignKey("CardId")]
        public virtual Card Card { get; set; }

        public void UpdateStatus(bool status)
        {
            this.IsCompleted = status;
        }

        public void UpdateName(string name)
        {
            this.Name = name;
        }
    }
}

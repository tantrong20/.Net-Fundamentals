using _468_.Net_Fundamentals.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace _468_.Net_Fundamentals.Domain.Entities
{
    [Table("Business")]
    public partial class Business 
    {
        public Business()
        {
            Cards = new HashSet<Card>();
        }

        public string Name { get; private set; }
        public int ProjectId { get; private set; }

        [ForeignKey("ProjectId")]
        public virtual Project Project { get; private set; }

        public virtual ICollection<Card> Cards { get; private set; }

    }
}

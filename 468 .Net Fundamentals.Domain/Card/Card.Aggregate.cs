using _468_.Net_Fundamentals.Domain.EnumType;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace _468_.Net_Fundamentals.Domain.Entities
{
    public partial class Card : EntityBase<int>
    {
        public Card(string name, int busId, float index) : this()
        {
            this.Name = name;
            this.BusinessId = busId;
            this.Index = index;
            Priority = TaskPriority.Normal;
            CreatedOn = DateTime.Now;
        }

        public void UpdateDuedate(DateTime date)
        {
            this.Duedate = date;
        }

        public void UpdateDescription(string description)
        {
            this.Description = description;
        }

        public void UpdatePriority(TaskPriority priority)
        {
            this.Priority = priority;
        }

        public void UpdateName(string name)
        {
            this.Name = name;
        }

        public void UpdateMovement(int busId, float index)
        {
            this.BusinessId = busId;
            this.Index = index;
        }

    }
}

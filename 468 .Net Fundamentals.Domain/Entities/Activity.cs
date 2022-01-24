using _468_.Net_Fundamentals.Domain.EnumType;
using System;
using System.Collections.Generic;
using System.Text;

namespace _468_.Net_Fundamentals.Domain.Entities
{
    public class Activity : EntityBase<int>
    {
        public int CardId { get; set; }
        public string UserId { get; set; }
        public AcctionEnumType Action { get; set; }
        public string? CurrentValue { get; set; }                                          
        public string? PreviousValue { get; set; }
        public DateTime OnDate { get; set; }
    }
}

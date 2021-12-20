using System;
using System.Collections.Generic;
using System.Text;

namespace _468_.Net_Fundamentals.Domain
{
    public class EnumType
    {
        public enum Role
        {
            Employee = 1,
            Manager = 2
        }

        public enum Priority
        {
            Normal = 1,
            Urgency = 2
        }

        public enum Status
        {
            Backlog = 1,
            Inprocess = 2, 
            Done = 3
        }
    }
}

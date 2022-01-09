using System;
using System.Collections.Generic;
using System.Text;

namespace _468_.Net_Fundamentals.Domain.ViewModels
{
    public class CardAssignVM
    {
        public int CardId { get; set; }
        public int AssignTo { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string ImagePath { get; set; }
    }
}

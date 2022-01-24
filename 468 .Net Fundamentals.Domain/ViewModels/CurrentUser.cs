using _468_.Net_Fundamentals.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace _468_.Net_Fundamentals.Domain.ViewModels
{
    public class CurrentUser : ICurrrentUser
    {
        public string Id { get; set; }
        public string UserName { get; set; }
    }
}

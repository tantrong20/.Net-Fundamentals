using System;
using System.Collections.Generic;
using System.Text;

namespace _468_.Net_Fundamentals.Domain.ViewModels.Authenticate
{
    public class RefreshToken
    {
        public Guid Id { get; set; }
        public string Token { get; set; }
        public string  UserId { get; set; }
    }
}

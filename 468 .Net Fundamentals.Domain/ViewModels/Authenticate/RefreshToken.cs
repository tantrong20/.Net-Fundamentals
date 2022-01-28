using _468_.Net_Fundamentals.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace _468_.Net_Fundamentals.Domain.ViewModels.Authenticate
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Token { get; set; }
        /*public string AccessTokenId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ExpiryTime { get; set; }
        public bool IsRevorked { get; set; }
        public bool IsUsed { get; set; }*/


    }
}

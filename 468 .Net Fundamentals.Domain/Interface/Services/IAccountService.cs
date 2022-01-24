using _468_.Net_Fundamentals.Domain.Base;
using _468_.Net_Fundamentals.Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace _468_.Net_Fundamentals.Domain.Interface.Services
{
    public interface IAccountService
    {
        public Task<IActionResult> Login(UserLoginVM userLoginVM);
        /*public Task<IActionResult> RevokeToken();*/
        public Task<IActionResult> RefreshToken(string refreshRequest);


        public Task<IActionResult> Register(UserRegistrationVM userRegistration);
       /* public Task<IActionResult> RegisterAdmin(UserRegistrationVM userRegistration);*/


    }
}

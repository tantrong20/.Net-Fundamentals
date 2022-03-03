using _468_.Net_Fundamentals.Domain.Base;
using _468_.Net_Fundamentals.Domain.ViewModels;
using _468_.Net_Fundamentals.Domain.ViewModels.Request;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace _468_.Net_Fundamentals.Domain.Interface.Services
{
    public interface IAccountService
    {
        Task<IActionResult> Login(UserLoginVM userLoginVM);
        /*public Task<IActionResult> RevokeToken();*/
        Task<IActionResult> Refresh(RefreshTokenRequest request);


        Task<IActionResult> Register(UserRegistrationVM userRegistration);
        /* public Task<IActionResult> RegisterAdmin(UserRegistrationVM userRegistration);*/

    }
}

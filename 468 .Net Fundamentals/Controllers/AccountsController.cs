using _468_.Net_Fundamentals.Domain.Base;
using _468_.Net_Fundamentals.Domain.Interface.Services;
using _468_.Net_Fundamentals.Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _468_.Net_Fundamentals.Controllers
{
    [Route("api/accounts")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private IAccountService _accountService;
        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationVM userRegistration)
        {
           return await _accountService.Register(userRegistration);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginVM userLoginVM)
        {
            return await _accountService.Login(userLoginVM);
        }

        [HttpPost("registeradmin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] UserRegistrationVM userRegistration)
        {
            return await _accountService.RegisterAdmin(userRegistration);
        }
    }
}

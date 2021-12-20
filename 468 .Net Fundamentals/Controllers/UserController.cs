using _468_.Net_Fundamentals.Domain.Entities;
using _468_.Net_Fundamentals.Domain.Interface.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _468_.Net_Fundamentals.Controllers
{
    [Route("users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }



        [HttpPost]
        public async Task Post(User user)
        {
            await _userService.Add(user);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> CheckExist(int id)
        {
            var result = await _userService.IsExistAsync(id);
            return Ok(result);
        }
    }
}

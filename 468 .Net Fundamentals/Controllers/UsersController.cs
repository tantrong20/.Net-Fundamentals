using _468_.Net_Fundamentals.Domain.Interface.Services;
using _468_.Net_Fundamentals.Domain.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace _468_.Net_Fundamentals.Controllers
{
    [Route("api/users")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            this._userService = userService;
        }

        [HttpPost("current")]
        public async Task<IActionResult> CurrentUser()
        {
            return await _userService.CurrentUser();
        }


        /*[HttpGet]
        public async Task<IList<UserVM>> GetAllExceptCurrentUser()
        {           
            return await _userService.GetAllExceptCurrentUser();
        }*/

        [HttpGet]
        public async Task<IList<UserVM>> GetAll()
        {
            return await _userService.GetAll();
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<UserVM> Get(string id)
        {
            return await _userService.Get(id);
        }

        [Route("/api/card/{cardId}/user")]
        [HttpPost]
        public async Task AddCardAssign(int cardId, [FromBody] string userId)
        {
            /*var identity = HttpContext.User.Identity as ClaimsIdentity;*/
            await _userService.AddCardAssign(cardId, userId);
        }


        [Route("/api/card/{cardId}/user")]
        [HttpGet]
        public async Task<IList<CardAssignVM>> GetAllCardAssign(int cardId)
        {
           return  await _userService.GetAllCardAssign(cardId);
        }

        [Route("/api/card/{cardId}/user/{userId}")]
        [HttpDelete]
        public async Task DeleteCardAssign(int cardId, string userId)
        {
            await _userService.DeleteCardAssign(cardId, userId);
        }
    }
}

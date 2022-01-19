using _468_.Net_Fundamentals.Domain.Interface.Services;
using _468_.Net_Fundamentals.Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _468_.Net_Fundamentals.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
       
        public UsersController(IUserService userService)
        {
            this._userService = userService;
        }

        [HttpGet]
        public async Task<IList<UserVM>> GetAll()
        {
            return await _userService.GetAll();
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<UserVM> Get(int id)
        {
            return await _userService.Get(id);
        }

        [Route("/api/card/{cardId}/user")]
        [HttpPost]
        public async Task AddCardAssign(int cardId, [FromBody] int userId)
        {
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
        public async Task DeleteCardAssign(int cardId, int userId)
        {
            await _userService.DeleteCardAssign(cardId, userId);
        }
    }
}

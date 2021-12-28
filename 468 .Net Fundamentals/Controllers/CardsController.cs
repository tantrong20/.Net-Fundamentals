using _468_.Net_Fundamentals.Domain.Entities;
using _468_.Net_Fundamentals.Domain.EnumType;
using _468_.Net_Fundamentals.Domain.Interface.Services;
using _468_.Net_Fundamentals.Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _468_.Net_Fundamentals.Controllers
{
    [Route("api/cards")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        private readonly ICardService _cardService;
        public CardsController(ICardService cardService)
        {
            _cardService = cardService;
        }

        [HttpPost]
        public async Task Create([FromBody] CardCreateVM request)
        {
            await _cardService.Create(request);
        }


        [HttpGet]
        public async Task<IList<CardVM>> GetAll(int busId)
        {
            return await _cardService.GetAllByBusiness(busId);
        }


        [HttpGet("{id}")]
        public async Task<CardVM> Get(int id)
        {
            return await _cardService.GetDetail(id);
        }


        [HttpPut("{id}/name")]
        public async Task UpdateName(int id, string newName)
        {
            await _cardService.UpdateName(id, newName);
        }

        [HttpPut("{id}/priority")]
        public async Task UpdatePriority(int id, TaskPriority newPriority)
        {
            await _cardService.UpdatePriority(id, newPriority);
        }

        [HttpPut("{id}/business")]
        public async Task UpdateBusiness(int id, int newBusinessId)
        {
            await _cardService.UpdateBusiness(id, newBusinessId);
        }

        [HttpPut("{id}/description")]
        public async Task UpdateDescription(int id, string newDescription)
        {
            await _cardService.UpdateName(id, newDescription);
        }

        [HttpPut("{id}/duedate")]
        public async Task UpdateDuedate(int id, DateTime newDuedate)
        {
            await _cardService.UpdateDuedate(id, newDuedate);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _cardService.Delete(id);
        }

        [HttpPost("{id}/tags/{tagId}")]
        public async Task AddTag(int id, int tagId)
        {
            await _cardService.AddTagOnCard(id, tagId);
        }

        [HttpDelete("{id}/tags/{tagId}")]
        public async Task DeleteTag(int id, int tagId)
        {
            await _cardService.DeleteTagOnCard(id, tagId);
        }

        [HttpGet("{id}/tags")]
        public async Task<IList<CardTagVM>> GetAllTag(int id)
        {
            return await _cardService.GetAllTagOnCard(id);
        }
    }
}

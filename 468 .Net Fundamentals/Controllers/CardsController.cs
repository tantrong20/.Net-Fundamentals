using _468_.Net_Fundamentals.Domain.Entities;
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
            return await _cardService.GetAll(busId);
        }


        [HttpGet("{id}")]
        public async Task<CardVM> Get(int id)
        {
            return await _cardService.Get(id);
        }


        [HttpPut("{id}")]
        public async Task Update(int id,CardVM cardVM)
        {
            await _cardService.Update(id, cardVM);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _cardService.Delete(id);
        }

        [HttpPost("{id}/tags/{tagId}")]
        public async Task AddTag(int id, int tagId)
        {
            await _cardService.AddTag(id, tagId);
        }

        [HttpDelete("{id}/tags/{tagId}")]
        public async Task DeleteTag(int id, int tagId)
        {
            await _cardService.DeleteTag(id, tagId);
        }

        [HttpGet("{id}/tags")]
        public async Task<IList<CardTag>> GetAllTag()
        {
            return await _cardService.GetAllTag();
        }
    }
}

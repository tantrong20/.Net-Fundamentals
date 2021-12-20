using _468_.Net_Fundamentals.Domain.Entities;
using _468_.Net_Fundamentals.Domain.Interface.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _468_.Net_Fundamentals.Controllers
{
    [Route("cards")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly ICardService _cardService;
        public CardController(ICardService cardService)
        {
            _cardService = cardService;
        }

        [HttpGet]
        public async Task<IList<Card>> ViewCards()
        {
            return await _cardService.GetAllCard();
        }

        [HttpGet("{id}")]
        public async Task<Card> ViewCard()
        {
            return await _cardService.GetCard();
        }

        [HttpPost]
        public async Task CreateCard(Card card)
        {
            await _cardService.AddCard(card);
        }
       
     
        [HttpPost("{id}/{userId}")]
        public async Task AssignUser([FromRoute] int id, [FromRoute] int userId)
        {
            await _cardService.AssignUser(id, userId);
        }

        [HttpPut("{id}")]
        public async Task UpdateCardPriority(int id, int priority, Card card)
        {
            await _cardService.UpdateCardPriority(id, priority);
        }

        
    }
}

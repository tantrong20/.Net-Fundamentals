﻿using _468_.Net_Fundamentals.Domain.Entities;
using _468_.Net_Fundamentals.Domain.EnumType;
using _468_.Net_Fundamentals.Domain.Interface.Services;
using _468_.Net_Fundamentals.Domain.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _468_.Net_Fundamentals.Controllers
{
    [Route("api/cards")]
    [ApiController]
    [Authorize]
    public class CardsController : ControllerBase
    {
        private readonly ICardService _cardService;
        public CardsController(ICardService cardService)
        {
            _cardService = cardService;
        }

        [HttpPost("/api/business/{busId}/cards")]
        public async Task Create(int busId, [FromBody] string name)
        {
            await _cardService.Create(busId, name);
        }

        [HttpGet("/api/business/{busId}/cards")]
        public async Task<IList<CardVM>> GetAll(int busId)
        {
            return await _cardService.GetAllByBusiness(busId);
        }


        [HttpGet("{id}")]
        public async Task<CardVM> Get(int id)
        {
            return await _cardService.GetDetail(id);
        }

        [HttpPut("{id}/movement")]
        public async Task CardMovement(int id,  [FromBody] CardMovementVM data)
        {
            await _cardService.CardMovement(id, data);
        }

        [HttpPut("{id}/name")]
        public async Task UpdateName(int id, [FromBody] string newName)
        {
            await _cardService.UpdateName(id, newName);
        }

        [HttpPut("{id}/priority")]
        public async Task UpdatePriority(int id, [FromBody] TaskPriority newPriority)
        {
            await _cardService.UpdatePriority(id, newPriority);
        }


        [HttpPut("{id}/description")]
        public async Task UpdateDescription(int id, [FromBody] string newDescription)
        {
            await _cardService.UpdateDescription(id, newDescription);
        }

        [HttpPut("{id}/duedate")]
        public async Task UpdateDuedate(int id, [FromBody] string newDuedate)
        {
            await _cardService.UpdateDuedate(id, newDuedate);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _cardService.Delete(id);
        }

       
    }
}

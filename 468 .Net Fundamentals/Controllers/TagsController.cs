﻿using _468_.Net_Fundamentals.Domain.Entities;
using _468_.Net_Fundamentals.Domain.EnumType;
using _468_.Net_Fundamentals.Domain.Interface.Services;
using _468_.Net_Fundamentals.Domain.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _468_.Net_Fundamentals.Controllers
{
    [Route("api/tags")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly ITagService _tagService;
        public TagsController(ITagService tagService)
        {
            _tagService = tagService;
        }


        [HttpPost("/api/project/{projectId}/tags")]
        [Authorize(Permissions.Tags.Create)]
        public async Task CreateOnProject(int projectId, [FromBody] string name)
        {
            await _tagService.CreateOnProject(projectId, name);
        }


        [HttpGet("/api/project/{projectId}/tags")]
        public async Task<IList<TagVM>> GetAllOnProject(int projectId)
        {
            return await _tagService.GetAllOnProject(projectId);
        }



        [HttpPost("/api/card/{cardId}/tags")]
        public async Task AddCardTag(int cardId, [FromBody] int tagId)
        {
            await _tagService.AddCardTag(cardId, tagId);
        }


        [HttpGet("/api/card/{cardId}/tags")]
        public async Task<IList<CardTagVM>> GetAllCardTag(int cardId)
        {
            return await _tagService.GetAllCardTag(cardId);
        }



        [HttpDelete("/api/card/{cardId}/tag/{tagId}")]
        public async Task DeleteCardTag(int cardId, int tagId)
        {
            await _tagService.DeleteCardTag(cardId, tagId);
        }

        [HttpPut("{id}")]
        public async Task Update(int id, string name)
        {
            await _tagService.Update(id, name);
        }

        [HttpDelete("{id}")]
        [Authorize(Permissions.Tags.Delete)]
        public async Task Delete(int id)
        {
            await _tagService.Delete(id);
        }


    }
}

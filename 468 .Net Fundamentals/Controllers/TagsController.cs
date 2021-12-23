using _468_.Net_Fundamentals.Domain.Entities;
using _468_.Net_Fundamentals.Domain.Interface.Services;
using _468_.Net_Fundamentals.Domain.ViewModels;
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

        [HttpPost]
        public async Task Create([FromBody] TagCreateVM request)
        {
            await _tagService.Create(request);
        }


        [HttpGet]
        public async Task<IList<TagVM>> GetAll([FromBody] int projectId)
        {
            return await _tagService.GetAll(projectId);
        }

        [HttpPut("{id}")]
        public async Task Update(int id, string name)
        {
            await _tagService.Update(id, name);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _tagService.Delete(id);
        }


    }
}

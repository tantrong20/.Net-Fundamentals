using _468_.Net_Fundamentals.Domain.Interface.Services;
using _468_.Net_Fundamentals.Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _468_.Net_Fundamentals.Controllers
{
    [Route("api/todos")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        private readonly ITodoService _todoService;
        public TodosController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        [HttpPost]
        public async Task Create([FromBody] TodoCreateVM request)
        {
            await _todoService.Create(request);
        }

        [HttpGet]
        public async Task<IList<TodoVM>> GetAll(int cardId)
        {
            return await _todoService.GetAll(cardId);
        }
         
        [HttpPut("{id}")]
        public async Task Update(int id, [FromBody] TodoVM todoVM)
        {
            await _todoService.Update(id, todoVM);
        }

        [HttpDelete("{id}")]
        public async Task Delete (int id)
        {
            await _todoService.Delete(id);
        }
    }
}

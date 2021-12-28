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
    [Route("api/businesses")]
    [ApiController]
    public class BusinessesController : ControllerBase
    {
        private IBusinessService _businessService;
        public BusinessesController(IBusinessService businessService)
        {
            _businessService = businessService;
        }

        [HttpPost]
        public async Task Create([FromBody] BusinessVM request)
        {
            await _businessService.Create(request);
        }

        [HttpGet]
        public async Task<IList<BusinessVM>> GetAllByProject(int projectId)
        {
            return await _businessService.GetAllByProject(projectId);
        }


        [Route("{id}")]
        [HttpPut]
        public async Task Update(int id, [FromBody] string name)
        {
            await _businessService.Update(id, name);
        }

        [Route("{id}")]
        [HttpDelete]
        public async Task Delete(int id)
        {
            await _businessService.Delete(id);
        }


    }
}

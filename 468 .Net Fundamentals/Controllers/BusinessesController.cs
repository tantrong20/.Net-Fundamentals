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
    [Route("api/business")]
    [ApiController]
    public class BusinessesController : ControllerBase
    {
        private IBusinessService _businessService;
        public BusinessesController(IBusinessService businessService)
        {
            _businessService = businessService;
        }


        // Business

        [HttpPost]
        public async Task CreateBusiness([FromBody] BusinessVM request)
        {
            await _businessService.CreateBusiness(request);
        }

        [HttpGet]
        public async Task<IList<BusinessVM>> GetAllBusiness(int projectId)
        {
            return await _businessService.GetAllBusiness(projectId);
        }


        [Route("{id}")]
        [HttpPut]
        public async Task UpdateBusiness(int id, [FromBody] string name)
        {
            await _businessService.UpdateBusiness(id, name);
        }

        [Route("{id}")]
        [HttpDelete]
        public async Task DeleteBusiness(int id)
        {
            await _businessService.DeleteBusiness(id);
        }


    }
}

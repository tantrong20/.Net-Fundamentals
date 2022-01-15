using _468_.Net_Fundamentals.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace _468_.Net_Fundamentals.Domain.Interface.Services
{
    public interface IActivityService
    {
        public Task<IList<Activity>> GetAllByCard(int id);
    }
}

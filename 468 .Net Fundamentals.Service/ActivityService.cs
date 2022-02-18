using _468_.Net_Fundamentals.Domain.Entities;
using _468_.Net_Fundamentals.Domain.Interface.Services;
using _468_.Net_Fundamentals.Domain.Repositories;
using _468_.Net_Fundamentals.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _468_.Net_Fundamentals.Service
{
    public class ActivityService :IActivityService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ActivityService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IList<Activity>> GetAllByCard(int id)
        {

            var activities = await _unitOfWork.Repository<Activity>().Query()
                .Where(_ => _.CardId == id)
                .ToListAsync();

            activities.Reverse();

            return activities;

        }
    }
}

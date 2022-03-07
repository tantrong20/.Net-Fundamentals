using _468_.Net_Fundamentals.Domain.Entities;
using _468_.Net_Fundamentals.Domain.EnumType;
using _468_.Net_Fundamentals.Domain.Interface;
using _468_.Net_Fundamentals.Domain.Repositories;
using System;
using System.Text;
using System.Threading.Tasks;

namespace _468_.Net_Fundamentals.Service.LogActivity
{
    public class LoggingUserActivity
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrrentUser _currrentUser;

        public LoggingUserActivity(ICurrrentUser currrentUser, IUnitOfWork unitOfWork)
        {
            _currrentUser = currrentUser;
            _unitOfWork = unitOfWork;
        }

        public async Task Save(int cardId, AcctionEnumType action, string? currentValue = null, string? previousValue = null)
        {
            var currentUserId = _currrentUser?.Id;

            var activity = new Activity
            {
                CardId = cardId,
                UserId = currentUserId,
                Action = action,
                CurrentValue = currentValue,
                PreviousValue = previousValue,
                OnDate = DateTime.Now
            };

            await _unitOfWork.Repository<Activity>().InsertAsync(activity);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}

using _468_.Net_Fundamentals.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace _468_.Net_Fundamentals.Domain.Interface.Services
{
    public interface IUserService
    {
        Task<bool> IsExistAsync(int id);
        Task<IList<User>> GetAll();
        Task Add(User user);
    }
}

using _468_.Net_Fundamentals.Domain.Entities;
using _468_.Net_Fundamentals.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace _468_.Net_Fundamentals.Domain.Interface.Services
{
    public interface ICardService
    {
        Task<IList<Card>> GetAllCard();
        Task<Card> GetCard();
        Task UpdateCardPriority(int Id, int priority);
        Task AddCard(Card card);

        Task AssignUser(int Id, int userId);

        Task AddTodo(int Id, Todo todo);

    }
}

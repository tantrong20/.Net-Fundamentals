using _468_.Net_Fundamentals.Domain.Entities;
using _468_.Net_Fundamentals.Domain.EnumType;
using _468_.Net_Fundamentals.Domain.Interface;
using _468_.Net_Fundamentals.Domain.Interface.Services;
using _468_.Net_Fundamentals.Domain.Repositories;
using _468_.Net_Fundamentals.Domain.ViewModels;
using _468_.Net_Fundamentals.Infrastructure;
using _468_.Net_Fundamentals.Service.LogActivity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace _468_.Net_Fundamentals.Service
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<AppUser> _userManager;
        private readonly ICurrrentUser _currrentUser;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserActivityLoger _userActivityLoger;

        public UserService(IUnitOfWork unitOfWork, UserManager<AppUser> userManager, ICurrrentUser user, RoleManager<IdentityRole> roleManager, UserActivityLoger userActivityLoger)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _currrentUser = user;
            _roleManager = roleManager;
            _userActivityLoger = userActivityLoger;
        }

        public async Task<IActionResult> CurrentUser()
        {
            try
            {
                var id = _currrentUser?.Id;
                var user = await _userManager.FindByIdAsync(id);

                var userRole = await _unitOfWork.Repository<IdentityUserRole<string>>().Query()
                    .Where(_ => _.UserId == _currrentUser.Id)
                    .FirstOrDefaultAsync();

                var role = await _roleManager.FindByIdAsync(userRole.RoleId);

                return new OkObjectResult(new
                {
                    id = user.Id,
                    userName = user.UserName,
                    email = user.Email,
                    imagePath = user.ImagePath,
                    role = role.Name
                });

            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public async Task AddCardAssign(int cardId, string userId)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                var card = await _unitOfWork.Repository<Card>().FindAsync(cardId);
                card.Assign(userId);
         
                var currentValue = userId.ToString();
                await _userActivityLoger.Log(cardId, AcctionEnumType.AssignUser, currentValue);

                await _unitOfWork.CommitTransaction();
            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackTransaction();
                throw e;
            }
        }

        public async Task<IList<CardAssignVM>> GetAllCardAssign(int cardId)
        {
            var cardAssignVM = await _unitOfWork.Repository<CardAssign>()
             .Query()
             .Where(_ => _.CardId == cardId)
             .Select(c => new CardAssignVM
             {
                 CardId = c.CardId,
                 AssignTo = c.AssignTo,
                 UserName = c.User.UserName,
                 Email = c.User.Email,
                 ImagePath = c.User.ImagePath
             }).ToListAsync();

            return cardAssignVM;
        }

        public async Task<UserVM> Get(string Id)
        {
            return await _unitOfWork.Repository<AppUser>()
              .Query()
              .Where(_ => _.Id == Id)
              .Select(u => new UserVM
              {
                  Id = u.Id,
                  UserName = u.UserName,
                  Email = u.Email,
                  ImagePath = u.ImagePath
              }).FirstOrDefaultAsync();
        }

        public async Task<IList<UserVM>> GetAllExceptCurrentUser()
        {
            return await _unitOfWork.Repository<AppUser>()
              .Query()
              .Where(_ => _.Id != _currrentUser.Id)
              .Select(u => new UserVM
              {
                  Id = u.Id,
                  UserName = u.UserName,
                  Email = u.Email,
                  ImagePath = u.ImagePath
              }).ToListAsync();
        }

        public async Task<IList<UserVM>> GetAll()
        {
            return await _unitOfWork.Repository<AppUser>()
               .Query()
               .Select(u => new UserVM
               {
                   Id = u.Id,
                   UserName = u.UserName,
                   Email = u.Email,
                   ImagePath = u.ImagePath
               }).ToListAsync();
        }

        public async Task DeleteCardAssign(int cardId, string userId)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                // Delete Card Assign
                var cardAssign = await _unitOfWork.Repository<CardAssign>()
                    .Query()
                    .Where(_ => _.CardId == cardId && _.AssignTo == userId)
                    .FirstOrDefaultAsync();
                await _unitOfWork.Repository<CardAssign>().DeleteAsync(cardAssign);

                // Save history
                var currentValue = cardAssign.AssignTo.ToString();
                await _userActivityLoger.Log(cardId, AcctionEnumType.RemoveAssignUser, currentValue);

                await _unitOfWork.CommitTransaction();

            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackTransaction();
                throw e;
            }

        }

    }
}

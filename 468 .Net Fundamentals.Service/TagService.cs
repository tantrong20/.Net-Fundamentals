using _468_.Net_Fundamentals.Domain.Entities;
using _468_.Net_Fundamentals.Domain.EnumType;
using _468_.Net_Fundamentals.Domain.Interface;
using _468_.Net_Fundamentals.Domain.Interface.Services;
using _468_.Net_Fundamentals.Domain.Repositories;
using _468_.Net_Fundamentals.Domain.ViewModels;
using _468_.Net_Fundamentals.Infrastructure;
using _468_.Net_Fundamentals.Service.LogActivity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _468_.Net_Fundamentals.Service
{
    public class TagService : ITagService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrrentUser _currrentUser;
        private readonly UserActivityLoger _userActivityLoger;

        public TagService(IUnitOfWork unitOfWork, ICurrrentUser currrentUser, UserActivityLoger userActivityLoger)
        {
            _unitOfWork = unitOfWork;
            _currrentUser = currrentUser;
            _userActivityLoger = userActivityLoger;
        }

        public async Task CreateOnProject(int projectId, string name)
        {
            try
            {
                var project  =await _unitOfWork.Repository<Project>().FindAsync(projectId);
                project.AddTag(name);

                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task<IList<TagVM>> GetAllOnProject(int projectId)
        {
            return await _unitOfWork.Repository<Tag>()
                .Query()
                .Where(_ => _.ProjectId == projectId)
                .Select(t => new TagVM
                {
                    Id = t.Id,
                    Name = t.Name,
                    ProjectId = t.ProjectId
                }).ToListAsync();
        }

        public async Task Update(int id, string name)
        {
            try
            {
                var tag = await _unitOfWork.Repository<Tag>().FindAsync(id);

                tag.Name = name;

                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task Delete(int id)
        {
            try
            {
                await _unitOfWork.Repository<Tag>().DeleteAsync(id);

                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task AddCardTag(int cardId, int tagId)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                // Add Card Tag
                var card = await _unitOfWork.Repository<Card>().FindAsync(cardId);
                card.AddTag(tagId);

                // Save Action
                var currentValue = tagId.ToString();
                await _userActivityLoger.Log(cardId, AcctionEnumType.AddLabel, currentValue);

                await _unitOfWork.CommitTransaction();
            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackTransaction();
                throw e;
            }
        }

        public async Task<IList<CardTagVM>> GetAllCardTag(int cardId)
        {
            return await _unitOfWork.Repository<CardTag>()
               .Query()
               .Where(_ => _.CardId == cardId)
               .Select(t => new CardTagVM
               {
                   CardId = t.CardId,
                   TagId = t.TagId,
                   TagName = t.Tag.Name
               }).ToListAsync();
        }

        public async Task DeleteCardTag(int cardId, int tagId)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                var cardTag = await _unitOfWork.Repository<CardTag>().Query()
                    .Where(_ => _.CardId == cardId && _.TagId == tagId)
                    .FirstOrDefaultAsync();
                await _unitOfWork.Repository<CardTag>().DeleteAsync(cardTag);

                // Save Action
                var currentValue = tagId.ToString();
                await _userActivityLoger.Log(cardId, AcctionEnumType.RemoveLabel, currentValue);

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

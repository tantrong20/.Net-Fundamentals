using _468_.Net_Fundamentals.Domain.Interface;
using _468_.Net_Fundamentals.Domain.Repositories;
using _468_.Net_Fundamentals.Domain.ViewModels.Authenticate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace _468_.Net_Fundamentals.Controllers.Authorization
{
    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrrentUser _currrentUser;



        public PermissionHandler(RoleManager<IdentityRole> roleManager, IUnitOfWork unitOfWork, ICurrrentUser currrentUser)
        {
            _roleManager = roleManager;
            _unitOfWork = unitOfWork;
            _currrentUser = currrentUser;
        }
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            try
            {
                if (context.User == null)
                {
                    return;
                }

                var role = await _unitOfWork.Repository<IdentityUserRole<string>>().Query()
                        .Where(_ => _.UserId == _currrentUser.Id)
                        .FirstOrDefaultAsync();

                var permissions = await _unitOfWork.Repository<IdentityRoleClaim<string>>().Query()
                                    .Where(_ => _.RoleId == role.RoleId)
                                    .Where(_ => _.ClaimValue == requirement.Permission)
                                    .AnyAsync();


                if (permissions)
                {
                    context.Succeed(requirement);
                    return;
                }

            }
            catch (Exception e)
            {

                throw e;
            }
          
        }
    }
}

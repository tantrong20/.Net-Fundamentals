using _468_.Net_Fundamentals.Domain.Entities;
using _468_.Net_Fundamentals.Domain.Interface;
using _468_.Net_Fundamentals.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace _468_.Net_Fundamentals.Controllers.ActionFilters
{
    public class ProjectMemberActionFilter : ActionFilterAttribute
    {
        private readonly ICurrrentUser _currrentUser;
        private readonly IUnitOfWork _unitOfWork;


        public ProjectMemberActionFilter(ICurrrentUser currrentUser, IUnitOfWork unitOfWork)
        {
            _currrentUser = currrentUser;
            _unitOfWork = unitOfWork;
        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //Check your condition here
            var projectRouteId = int.Parse(filterContext.ActionArguments["id"].ToString());
            var project = _unitOfWork.Repository<Project>()
                .Query()
                .Where(_ => _.Id == projectRouteId)
                .FirstOrDefault();
            var currentUserId = _currrentUser?.Id;

            if(project == null)
            {
                filterContext.Result = new StatusCodeResult(401);
            }
            else if (project?.CreatedBy != currentUserId)
            {
                //Create your result
                filterContext.Result = new StatusCodeResult(403);
            }
            else
                base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
        }

    }
}

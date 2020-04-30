using StoreBuy.Repositories;
using StoreBuy.UnitofWork.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace StoreBuy.App_Start
{
    public class UnitOfWorkActionFilter : ActionFilterAttribute
    {
        public IUnitOfWork UnitOfWork { get; set; }
        
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            UnitOfWork = actionContext.Request.GetDependencyScope().GetService(typeof(IUnitOfWork)) as IUnitOfWork;
            UnitOfWork.BeginTransaction();
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            UnitOfWork = actionExecutedContext.Request.GetDependencyScope().GetService(typeof(IUnitOfWork)) as IUnitOfWork;
            if (actionExecutedContext.Exception == null)
            {
                UnitOfWork.Commit();
            }
            else
            {
                UnitOfWork.Rollback();
            }
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SmartCity.CitizenAccount.Api.Common.Exceptions;
using SmartCity.CitizenAccount.Api.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SmartCity.CitizenAccount.Filters
{
    public class ApiExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is NotFoundException)
            {
                var ex = context.Exception as NotFoundException;
                context.Exception = null;

                context.Result = new JsonResult(new ExceptionModel { Message = ex.Message });
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
            }
            else if (context.Exception is BadRequestException)
            {
                var ex = context.Exception as BadRequestException;
                context.Exception = null;

                context.Result = new JsonResult(new ExceptionModel { Message = ex.Message });
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            else if (context.Exception is UnauthorizedAccessException)
            {
                context.Result = new JsonResult(new ExceptionModel { Message = context.Exception.Message });
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            }
            else if (context.Exception is ForbiddenException)
            {
                var ex = context.Exception as ForbiddenException;
                context.Exception = null;

                context.Result = new JsonResult(new ExceptionModel { Message = context.Exception.Message });
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            }

            base.OnException(context);
        }
    }
}

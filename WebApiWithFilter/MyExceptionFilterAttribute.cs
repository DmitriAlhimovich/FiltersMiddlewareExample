using System;
using System.IO;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApiWithFilter
{
    public class MyExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            base.OnException(context);

            File.AppendAllText("errorLog.txt", $"Error occurred at {DateTime.Now}. Message: {context.Exception.Message}");
        }
    }
}
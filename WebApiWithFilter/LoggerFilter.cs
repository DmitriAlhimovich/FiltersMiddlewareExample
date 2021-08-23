using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApiWithFilter
{
    public class LoggerFilter : IActionFilter
    {
        private const string LogFilePath = "log.txt";
        public void OnActionExecuting(ActionExecutingContext context)
        {
            // Do something before the action executes.
            
            File.AppendAllText(LogFilePath, $"Action executing. Path: {context.HttpContext.Request.Path} \n");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // Do something after the action executes.
            File.AppendAllText(LogFilePath, $"Action executed. Path: {context.HttpContext.Request.Path} at {DateTime.Now} \n");
        }
    }
}

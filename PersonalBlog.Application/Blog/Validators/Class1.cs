using Castle.DynamicProxy;
using DNT.Deskly.Dependency;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalBlog.Application.Blog.Validators
{
    public class ExceptionHandlingInterceptor : IInterceptor, IScopedDependency
    {
        private readonly ILogger<ExceptionHandlingInterceptor> _logger;

        public ExceptionHandlingInterceptor(ILogger<ExceptionHandlingInterceptor> logger)
        {
            _logger = logger;
        }

        public void Intercept(IInvocation invocation)
        {
            try
            {
                invocation.Proceed(); //فراخوانی متد اصلی در اینجا صورت می‌گیرد
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "An unhandled exception has been occurred.");
            }
        }
    }
}
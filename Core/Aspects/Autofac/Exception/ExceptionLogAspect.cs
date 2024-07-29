using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;
using Castle.DynamicProxy;
using Core.Constants.Messages;
using Core.CrossCuttingConcerns.Logging.Log4Net;
using Core.CrossCuttingConcerns.Logging;
using Core.Utilities.Interceptors;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Core.Utilities.Service;
using Newtonsoft.Json;
using Microsoft.Extensions.Primitives;
using System.Security.Claims;

namespace Core.Aspects.Autofac.Exception
{
    public class ExceptionLogAspect : MethodInterception
    {
        private LoggerServiceBase _loggerServiceBase;
        private IHttpContextAccessor _httpContextAccessor;
        public ExceptionLogAspect(Type loggerService)
        {
            if (loggerService.BaseType != typeof(LoggerServiceBase))
            {
                throw new System.Exception(AspectMessages.WrongLoggerType);
            }

            _loggerServiceBase = (LoggerServiceBase)Activator.CreateInstance(loggerService);
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }
        protected override void OnException(IInvocation invocation, System.Exception e)
        {
            LogDetailWithException logDetailWithException = GetLogDetail(invocation);
            logDetailWithException.ExceptionMessage = e.ToString();
            var serviceName = invocation.Method.DeclaringType.Name;
            _loggerServiceBase.Error(logDetailWithException);
        }
        private LogDetailWithException GetLogDetail(IInvocation invocation)
        {
            var logParameters = new List<LogParameter>();

            for (int i = 0; i < invocation.Arguments.Length; i++)
            {
                logParameters.Add(new LogParameter
                {
                    Name = invocation.GetConcreteMethod().GetParameters()[i].Name,
                    Value = invocation.Arguments[i],
                    Type = invocation.Arguments[i].GetType().Name,
                });
            }

            var registrationNumber = 0;

            if (_httpContextAccessor != null)
            {
                if (_httpContextAccessor.HttpContext != null)
                {
                    if (_httpContextAccessor.HttpContext.User != null)
                    {
                        if (_httpContextAccessor.HttpContext.User.Claims != null)
                        {
                            if (_httpContextAccessor.HttpContext.User.Claims.Count() > 0)
                            {
                                var registrationNumberString = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.SerialNumber).Value;
                                if (!string.IsNullOrEmpty(registrationNumberString))
                                {
                                    registrationNumber = Convert.ToInt32(registrationNumberString);
                                }
                            }
                        }
                    }
                }
            }

            var remoteIpAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();

            var logDetailWithException = new LogDetailWithException
            {
                MethodName = $"{invocation.Method.DeclaringType.Name}.{invocation.Method.Name}",
                LogParameters = logParameters,
                Date = DateTime.UtcNow,
                RemoteIpAddress = remoteIpAddress,
                RegistrationNumber =registrationNumber,
                Response = JsonConvert.SerializeObject(invocation.ReturnValue, Formatting.None,
                        new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        })
        };

            return logDetailWithException;
        }










    }
}

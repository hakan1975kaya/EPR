using Castle.DynamicProxy;
using Core.Constants.Messages;
using Core.CrossCuttingConcerns.Logging;
using Core.CrossCuttingConcerns.Logging.Log4Net;
using Core.Utilities.Interceptors;
using Core.Utilities.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System.Security.Claims;

namespace Core.Aspects.Autofac.Logging
{
    public class LogAspect : MethodInterception
    {
        private LoggerServiceBase _loggerServiceBase;
        private IHttpContextAccessor _httpContextAccessor;
        public LogAspect(Type loggerService)
        {
            if (loggerService.BaseType != typeof(LoggerServiceBase))
            {
                throw new System.Exception(AspectMessages.WrongLoggerType);
            }

            _loggerServiceBase = (LoggerServiceBase)Activator.CreateInstance(loggerService);

            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }
        protected override void OnAfter(IInvocation invocation)
        {
            var serviceName = invocation.Method.DeclaringType.Name;
            _loggerServiceBase.Info(GetLogDetail(invocation));
        }
        private LogDetail GetLogDetail(IInvocation invocation)
        {
            var logParameters = new List<LogParameter>();
            for (int i = 0; i < invocation.Arguments.Length; i++)
            {
                logParameters.Add(new LogParameter
                {
                    Name = invocation.GetConcreteMethod().GetParameters()[i].Name,
                    Value = invocation.Arguments[i],
                    Type = invocation.Arguments[i].GetType().Name
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

            var logDetail = new LogDetail
            {
                MethodName = $"{invocation.Method.DeclaringType.Name}.{invocation.Method.Name}",
                LogParameters = logParameters,
                Date = DateTime.UtcNow,
                RemoteIpAddress = remoteIpAddress,
                RegistrationNumber = registrationNumber,
                Response = JsonConvert.SerializeObject(invocation.ReturnValue, Formatting.None,
                        new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        })
            };
            return logDetail;
        }













    }
}
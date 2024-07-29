using Castle.DynamicProxy;
using Core.Constants.Messages;
using Core.Extensions;
using Core.Utilities.Interceptors;
using Core.Utilities.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Business.BusinessAspect.Autofac
{
    public class SecurityAspect : MethodInterception
    {
        string[] _roles;
        IHttpContextAccessor _httpContextAccessor;
        public SecurityAspect(string roles)
        {
            _roles = roles.Split(',');
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }
        protected override void OnBefore(IInvocation invocation)
        {
            if (_roles != null)
            {
                var userRoles = _httpContextAccessor.HttpContext.User.ClaimRoles();
                if (_roles.Length > 0)
                {
                    foreach (var _role in _roles)
                    {
                        if (userRoles.Contains(_role))
                        {
                            return;
                        }
                    }
                }
                throw new Exception(AspectMessages.AuthorizationDenied);
            }
          
        }










    }
}

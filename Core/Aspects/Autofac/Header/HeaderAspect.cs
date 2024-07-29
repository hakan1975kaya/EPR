using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;

namespace Core.Aspects.Autofac.Header
{
    public class HeaderAspect : MethodInterception
    {
        private IHttpContextAccessor _httpContextAccessor;
        private string _headerName;
        public HeaderAspect(string headerName)
        {
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
            _headerName = headerName;
        }
        protected override void OnBefore(IInvocation invocation)
        {
            var isHeaderValueExist = _httpContextAccessor.HttpContext.Request.Headers.TryGetValue(_headerName, out StringValues headerValue);
            if (!isHeaderValueExist)
            {
                throw new System.Exception(_headerName + " değerini headerdan göndermediniz.");
            }
        }
    }
}

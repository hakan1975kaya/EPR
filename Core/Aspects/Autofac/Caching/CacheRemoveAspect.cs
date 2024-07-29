using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Interceptors;
using Core.Utilities.Service;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Aspects.Autofac.Caching
{
    public  class CacheRemoveAspect:MethodInterception
    {
        string _pattern;
        ICacheService _cacheService;
        public CacheRemoveAspect(string pattern)
        {
            _pattern = pattern;
            _cacheService = ServiceTool.ServiceProvider.GetService<ICacheService>();
        }
        protected override void OnSuccess(IInvocation invocation)
        {
          _cacheService.RemoveByPattern(_pattern);
        }









    }
}

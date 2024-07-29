using System;
using System.Collections.Generic;
using System.Text;
using Core.IoC.Abstract;
using Core.Utilities.Service;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDependencyResolvers(this IServiceCollection services,ICoreModule[] modules)
        {
            if(modules != null)
            {
                if(modules.Count()>0)
                {
                    foreach (var module in modules)
                    {
                        module.Load(services);
                    }
                }
            }
            return ServiceTool.Create(services);
        }











    }
}

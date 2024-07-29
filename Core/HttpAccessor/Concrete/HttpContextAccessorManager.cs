using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Core.HttpAccessor.Abstract;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Core.Utilities.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Core.HttpAccessor.Concrete
{
    public class HttpContextAccessorManager: IHttpContextAccessorService
    {
        private IHttpContextAccessor _httpContextAccessor;
        public HttpContextAccessorManager()
        {
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }
        public async Task<IDataResult<int>> GetRegistrationNumber()
        {
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
            return new SuccessDataResult<int>(registrationNumber);
        }
    }
}

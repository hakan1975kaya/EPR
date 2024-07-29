using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Results.Abstract;

namespace Core.HttpAccessor.Abstract
{
    public interface IHttpContextAccessorService
    {
        Task<IDataResult<int>> GetRegistrationNumber();
    }
}

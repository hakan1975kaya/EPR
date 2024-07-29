using Core.Entities.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.Dtos.HcpDtos
{
    public class HcpUploadRequestDto : IDto
    {
        public IFormFile File { get; set; }
    }
}

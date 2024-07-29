using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities.Abstract;

namespace Entities.Concrete.Dtos.HcpDtos
{
    public class HcpRequestDto : IDto
    {
        public string? FileName { get; set; }
    }
}

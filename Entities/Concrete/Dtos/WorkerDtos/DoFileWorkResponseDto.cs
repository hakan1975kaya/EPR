using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities.Abstract;

namespace Entities.Concrete.Dtos.WorkerDtos
{
    public class DoFileWorkResponseDto : IDto
    {
        public int Count { get; set; }
        public decimal TotalAmount { get; set; }
    }
}

using Core.Entities.Abstract;
using Entities.Concrete.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.Dtos.WebLogDtos.WebLogUpdateDtos
{
    public class WebLogUpdateRequestDto :IDto
    {
        public long Id { get; set; }
        public string Detail { get; set; }
        public DateTime Date { get; set; }
        public AuditEnum Audit { get; set; }
    }
}

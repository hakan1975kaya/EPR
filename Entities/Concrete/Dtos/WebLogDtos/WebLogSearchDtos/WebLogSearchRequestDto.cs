using Core.Entities.Abstract;
using Entities.Concrete.Enums;

namespace Entities.Concrete.Dtos.WebLogDtos.WebLogSearchDtos
{
    public class WebLogSearchRequestDto : IDto
    {
        public AuditEnum Audit { get; set; }
        public string Filter { get; set; }
    }
}

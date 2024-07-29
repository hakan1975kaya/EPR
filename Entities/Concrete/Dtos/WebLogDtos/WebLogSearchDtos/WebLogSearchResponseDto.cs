using Core.Entities.Abstract;
using Entities.Concrete.Enums;

namespace Entities.Concrete.Dtos.WebLogDtos.WebLogSearchDtos
{
    public class WebLogSearchResponseDto : IDto
    {
        public long Id { get; set; }
        public string Detail { get; set; }
        public DateTime Date { get; set; }
        public AuditEnum Audit { get; set; }
    }
}

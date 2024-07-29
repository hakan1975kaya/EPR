using Core.Entities.Abstract;
using Entities.Concrete.Enums;

namespace Entities.Concrete.Dtos.ApiLogDtos.ApiLogSearchDtos
{
    public class ApiLogSearchRequestDto : IDto
    {
        public AuditEnum Audit { get; set; }
        public string Filter { get; set; }
    }
}

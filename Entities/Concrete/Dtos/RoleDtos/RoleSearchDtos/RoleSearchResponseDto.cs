using Core.Entities.Abstract;
using Entities.Concrete.Enums;

namespace Entities.Concrete.Dtos.RoleDtos.RoleSearchDtos
{
    public class RoleSearchResponseDto : IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Optime { get; set; }
        public bool IsActive { get; set; }
    }
}

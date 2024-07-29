using Core.Entities.Abstract;

namespace Entities.Concrete.Dtos.RoleDtos.RoleSearchDtos
{
    public class RoleSearchRequestDto : IDto
    {
        public string Filter { get; set; }
    }
}

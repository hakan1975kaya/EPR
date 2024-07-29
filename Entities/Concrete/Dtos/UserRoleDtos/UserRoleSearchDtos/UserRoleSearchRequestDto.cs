using Core.Entities.Abstract;

namespace Entities.Concrete.Dtos.UserRoleDtos.UserRoleSearchDtos
{
    public class UserRoleSearchRequestDto : IDto
    {
        public string Filter { get; set; }
    }
}

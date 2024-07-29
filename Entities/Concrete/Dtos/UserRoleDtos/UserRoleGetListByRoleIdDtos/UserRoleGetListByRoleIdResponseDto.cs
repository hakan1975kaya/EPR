using Core.Entities.Abstract;

namespace Entities.Concrete.Dtos.UserRoleDtos.UserRoleGetListByCorporateIdDtos
{
    public class UserRoleGetListByRoleIdResponseDto : IDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RegistrationNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public DateTime Optime { get; set; }
        public bool IsActive { get; set; }

    }
}


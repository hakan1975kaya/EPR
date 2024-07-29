using Core.Entities.Abstract;
using Core.Entities.Concrete.Entities;
using Entities.Concrete.Entities;
using Entities.Concrete.Enums.GeneralEnums;

namespace Entities.Concrete.Dtos.UserRoleDtos.UserRoleSaveDtos
{
    public class UserRoleSaveRequestDto : IDto
    {
        public UserRole UserRole { get; set; }
        public SaveTypeEnum SaveType { get; set; }
    }
}

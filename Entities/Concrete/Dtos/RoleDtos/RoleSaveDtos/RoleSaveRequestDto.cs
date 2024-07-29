using Core.Entities.Abstract;
using Core.Entities.Concrete.Entities;
using Entities.Concrete.Enums.GeneralEnums;

namespace Entities.Concrete.Dtos.RoleDtos.RoleSaveDtos
{
    public class RoleSaveRequestDto : IDto
    {
        public Role Role { get; set; }
        public SaveTypeEnum SaveType { get; set; }
    }
}

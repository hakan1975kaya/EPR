using Core.Entities.Abstract;
using Core.Entities.Concrete.Entities;
using Entities.Concrete.Enums.GeneralEnums;

namespace Entities.Concrete.Dtos.UserDtos.UserSaveDtos
{
    public class UserSaveRequestDto : IDto
    {
        public UserDto User { get; set; }
        public bool IsPasswordSeted { get; set; }
        public SaveTypeEnum SaveType { get; set; }
    }
}

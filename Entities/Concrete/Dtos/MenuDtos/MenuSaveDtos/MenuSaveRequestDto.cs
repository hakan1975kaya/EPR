using Core.Entities.Abstract;
using Core.Entities.Concrete.Entities;
using Entities.Concrete.Entities;
using Entities.Concrete.Enums.GeneralEnums;

namespace Entities.Concrete.Dtos.MenuDtos.MenuSaveDtos
{
    public class MenuSaveRequestDto : IDto
    {
        public Menu Menu { get; set; }
        public SaveTypeEnum SaveType { get; set; }
    }
}

using Core.Entities.Abstract;
using Core.Entities.Concrete.Entities;
using Entities.Concrete.Entities;
using Entities.Concrete.Enums.GeneralEnums;

namespace Entities.Concrete.Dtos.CorporateDtos.CorporateSaveDtos
{
    public class CorporateSaveRequestDto : IDto
    {
        public Corporate Corporate { get; set; }
        public SaveTypeEnum SaveType { get; set; }
    }
}

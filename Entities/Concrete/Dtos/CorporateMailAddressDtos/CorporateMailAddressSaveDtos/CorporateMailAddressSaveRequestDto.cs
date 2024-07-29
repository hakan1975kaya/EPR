using Core.Entities.Abstract;
using Core.Entities.Concrete.Entities;
using Entities.Concrete.Enums.GeneralEnums;

namespace Entities.Concrete.Dtos.CorporateMailAddressDtos.CorporateMailAddressSaveDtos
{
    public class CorporateMailAddressSaveRequestDto : IDto
    {
        public CorporateMailAddress CorporateMailAddress { get; set; }
        public SaveTypeEnum SaveType { get; set; }
    }
}

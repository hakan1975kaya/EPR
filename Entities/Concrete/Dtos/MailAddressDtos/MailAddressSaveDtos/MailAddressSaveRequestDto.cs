using Core.Entities.Abstract;
using Core.Entities.Concrete.Entities;
using Entities.Concrete.Enums.GeneralEnums;

namespace Entities.Concrete.Dtos.MailAddressDtos.MailAddressSaveDtos
{
    public class MailAddressSaveRequestDto : IDto
    {
        public MailAddress MailAddress { get; set; }
        public SaveTypeEnum SaveType { get; set; }
    }
}

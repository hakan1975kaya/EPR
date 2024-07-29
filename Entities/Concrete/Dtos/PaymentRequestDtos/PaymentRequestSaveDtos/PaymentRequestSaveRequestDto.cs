using Core.Entities.Abstract;
using Core.Entities.Concrete.Entities;
using Entities.Concrete.Enums.GeneralEnums;

namespace Entities.Concrete.Dtos.PaymentRequestDtos.PaymentRequestSaveDtos
{
    public class PaymentRequestSaveRequestDto : IDto
    {
        public PaymentRequest PaymentRequest { get; set; }
        public List<PaymentRequestDetail> PaymentRequestDetails { get; set; }
        public SaveTypeEnum SaveType { get; set; }
    }
}

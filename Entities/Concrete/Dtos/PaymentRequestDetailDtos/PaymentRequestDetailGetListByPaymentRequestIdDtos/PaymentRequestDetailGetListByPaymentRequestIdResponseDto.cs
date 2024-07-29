using Core.Entities.Abstract;
using Entities.Concrete.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.Dtos.PaymentRequestDetailDtos.PaymentRequestDetailGetListByPaymentRequestIdDtos
{
    public class PaymentRequestDetailGetListByPaymentRequestIdResponseDto : IDto
    {
        public int Id { get; set; }
        public int PaymentRequestId { get; set; }
        public string ReferenceNumber { get; set; }
        public long AccountNumber { get; set; }
        public long CustomerNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal PaymentAmount { get; set; }
        public DateTime CardDepositDate { get; set; }
        public string Explanation { get; set; }
        public DateTime Optime { get; set; }
        public bool IsActive { get; set; }
    }
}

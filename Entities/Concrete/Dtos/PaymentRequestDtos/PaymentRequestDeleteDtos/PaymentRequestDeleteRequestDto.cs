using Core.Entities.Abstract;
using Entities.Concrete.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.Dtos.PaymentRequestDtos.PaymentRequestDeleteDtos
{
    public class PaymentRequestDeleteRequestDto : IDto
    {
        public int Id { get; set; }
        public string RequestNumber { get; set; }
        public int CorporateId { get; set; }
        public int UserId { get; set; }
        public int FileId { get; set; }
        public MoneyTypeEnum MoneyType { get; set; }
        public StatusEnum Status { get; set; }
        public DateTime Optime { get; set; }
        public bool IsActive { get; set; }

    }
}

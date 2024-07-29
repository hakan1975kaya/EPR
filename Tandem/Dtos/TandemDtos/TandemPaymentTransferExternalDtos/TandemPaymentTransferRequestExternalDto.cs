using Entities.Concrete.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Tandem.Dtos.TandemDtos.TandemPaymentTransferExternalDtos
{
    public class TandemPaymentTransferRequestExternalDto
    {
        public string RequestNumber { get; set; }
        public int CorporateCode { get; set; }
        public int RegistrationNumber { get; set; }
        public MoneyTypeEnum MoneyType { get; set; }
        public List<PaymentExternal> PaymentExternals { get; set; }
    }
}




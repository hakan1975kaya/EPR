using Entities.Concrete.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Tandem.Dtos.TandemDtos.TandemPaymentTransferInternalDtos
{
    public class TandemPaymentTransferRequestInternalDto
    {
        [JsonPropertyName("reqNo")]
        public string RequestNumber { get; set; }

        [JsonPropertyName("kurumKodu")]
        public int CorporateCode { get; set; }

        [JsonPropertyName("sicilNo")]
        public int RegistrationNumber { get; set; }

        [JsonPropertyName("paraTuru")]
        public string MoneyType { get; set; }

        [JsonPropertyName("kayitList")]
        public List<PaymentInternal> PaymentInternals { get; set; }
    }
}




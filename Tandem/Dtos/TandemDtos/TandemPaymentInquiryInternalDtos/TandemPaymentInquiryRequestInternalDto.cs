using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Entities.Concrete.Enums;

namespace Tandem.Dtos.TandemDtos.TandemPaymentInquiryInternalDtos
{
    public class TandemPaymentInquiryRequestInternalDto
    {
        [JsonPropertyName("kurumKodu")]
        public int CorporateCode { get; set; }

        [JsonPropertyName("durum")]
        public StatusEnum Status { get; set; }
    }
}


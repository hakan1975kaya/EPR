using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Entities.Concrete.Enums;

namespace Tandem.Dtos.TandemDtos.TandemPaymentInquiryInternalDtos
{
    public class SummaryInternal
    {
        [JsonPropertyName("reqNo")]
        public string RequestNumber { get; set; }

        [JsonPropertyName("kurumKodu")]
        public int CorporateCode { get; set; }

        [JsonPropertyName("durum")]
        public StatusEnum Status { get; set; }

        [JsonPropertyName("yuklemeTarihi")]
        public int UploadDate { get; set; }

        [JsonPropertyName("adet")]
        public int Quantity { get; set; }

        [JsonPropertyName("tutar")]
        public decimal Amount { get; set; }

        [JsonPropertyName("sicil")]
        public int RegistrationNumber { get; set; }

        [JsonPropertyName("sisGirTarih")]
        public int SystemEnteredDate { get; set; }

        [JsonPropertyName("sisGirSaat")]
        public int SystemEnteredTime { get; set; }

        [JsonPropertyName("sisGirSicil")]
        public int SystemEnteredRegistrationNumber { get; set; }
    }
}

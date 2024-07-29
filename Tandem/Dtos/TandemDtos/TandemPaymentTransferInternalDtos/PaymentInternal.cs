using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Tandem.Dtos.TandemDtos.TandemPaymentTransferInternalDtos
{
    public class PaymentInternal
    {

        [JsonPropertyName("refNo")]
        public string ReferenceNumber { get; set; }

        [JsonPropertyName("hesapNo")]
        public long AccountNumber { get; set; }

        [JsonPropertyName("musteriNo")]
        public long CustomerNumber { get; set; }

        [JsonPropertyName("telNo")]
        public string PhoneNumber { get; set; }

        [JsonPropertyName("ad")]
        public string FirstName { get; set; }

        [JsonPropertyName("soyad")]
        public string LastName { get; set; }

        [JsonPropertyName("odemeTutari")]
        public decimal PaymentAmount { get; set; }

        [JsonPropertyName("kartYatTarih")]
        public string CardDepositDate { get; set; }

        [JsonPropertyName("aciklama")]
        public string Explanation { get; set; }
    }
}

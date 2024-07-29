using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Entities.Concrete.Enums;

namespace Tandem.Dtos.TandemDtos.TandemCorporateDefineInternalDtos
{
    public class CorporateInternal
    {
        [JsonPropertyName("kurumKodu")]
        public int CorporateCode { get; set; }

        [JsonPropertyName("kurumAdi")]
        public string CorporateName { get; set; }

        [JsonPropertyName("kurumHesapNo")]
        public int CorporateAccountNo { get; set; }

        [JsonPropertyName("paraTuru")]
        public MoneyTypeEnum MoneyType { get; set; }

        [JsonPropertyName("dosyaPrefix")]
        public string Prefix { get; set; }
    }
}

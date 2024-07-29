using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Entities.Concrete.Enums;

namespace Tandem.Dtos.TandemDtos.TandemCorporateDefineExternalDtos
{
    public class CorporateExternal
    {
        public int CorporateCode { get; set; }
        public string CorporateName { get; set; }
        public int CorporateAccountNo { get; set; }
        public MoneyTypeEnum MoneyType { get; set; }
        public string Prefix { get; set; }
    }
}

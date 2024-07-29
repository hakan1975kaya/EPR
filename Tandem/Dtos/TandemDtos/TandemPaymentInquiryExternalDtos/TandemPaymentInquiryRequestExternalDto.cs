using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Entities.Concrete.Enums;

namespace Tandem.Dtos.TandemDtos.TandemPaymentInquiryExternalDtos
{
    public class TandemPaymentInquiryRequestExternalDto
    {
        public int CorporateCode { get; set; }
        public StatusEnum Status { get; set; }
    }
}


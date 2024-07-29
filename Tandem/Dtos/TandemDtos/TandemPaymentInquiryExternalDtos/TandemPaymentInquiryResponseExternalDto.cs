using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Tandem.Dtos.TandemDtos.TandemPaymentInquiryExternalDtos
{
    public class TandemPaymentInquiryResponseExternalDto
    {
        public string ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public List<SummaryExternal> SummaryExternals { get; set; }
    }
}


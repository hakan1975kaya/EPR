using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Tandem.Dtos.TandemDtos.TandemPaymentUpdateInternalDtos
{
    public class TandemPaymentUpdateResponseInternalDto
    {
        [JsonPropertyName("responseCode")]
        public string ResponseCode { get; set; }

        [JsonPropertyName("responseMessage")]
        public string ResponseMessage { get; set; }
    }
}




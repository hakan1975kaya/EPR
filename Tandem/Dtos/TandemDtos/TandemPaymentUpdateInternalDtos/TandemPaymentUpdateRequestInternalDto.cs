using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Tandem.Dtos.TandemDtos.TandemPaymentUpdateInternalDtos
{
    public class TandemPaymentUpdateRequestInternalDto
    {
        [JsonPropertyName("requestList")]
        public List<RequestListInternal> RequestListInternals { get; set; }

    }
}


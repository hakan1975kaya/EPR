using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Tandem.Dtos.TandemDtos.TandemPaymentUpdateExternalDtos
{
    public class TandemPaymentUpdateRequestExternalDto
    {
        public List<RequestListExternal> RequestListExternals { get; set; }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Concrete.Enums;

namespace Tandem.Dtos.TandemDtos.TandemPaymentUpdateExternalDtos
{
    public class RequestListExternal
    {
        public string RequestNumber { get; set; }
        public int CorporateCode { get; set; }
        public StatusEnum Status { get; set; }
        public int RegistrationNumber { get; set; }
    }
}

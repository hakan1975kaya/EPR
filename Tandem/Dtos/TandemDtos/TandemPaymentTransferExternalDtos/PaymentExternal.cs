using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Tandem.Dtos.TandemDtos.TandemPaymentTransferExternalDtos
{
    public class PaymentExternal
    {

        public string ReferenceNumber { get; set; }
        public long AccountNumber { get; set; }
        public long CustomerNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal PaymentAmount { get; set; }
        public DateTime CardDepositDate { get; set; }
        public string Explanation { get; set; }
    }
}

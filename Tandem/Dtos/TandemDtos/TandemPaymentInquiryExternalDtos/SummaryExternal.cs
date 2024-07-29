using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Entities.Concrete.Enums;

namespace Tandem.Dtos.TandemDtos.TandemPaymentInquiryExternalDtos
{
    public class SummaryExternal
    {
        public string RequestNumber { get; set; }
        public int CorporateCode { get; set; }
        public StatusEnum Status { get; set; }
        public DateTime UploadDate { get; set; }
        public int Quantity { get; set; }
        public decimal Amount { get; set; }
        public int RegistrationNumber { get; set; }
        public DateTime SystemEnteredDate { get; set; }
        public TimeSpan SystemEnteredTime { get; set; }
       public int SystemEnteredRegistrationNumber { get; set; }
    }
}

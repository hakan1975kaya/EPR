using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.Dtos.PaymentRequestSummaryDtos.PaymentRequestSummaryChartByCorporateIdYear
{
    public class PaymentRequestSummaryAmountByCorporateIdYearRequestDto
    {
        public int CorporateId { get; set; }
        public int Year { get; set; }
    }
}

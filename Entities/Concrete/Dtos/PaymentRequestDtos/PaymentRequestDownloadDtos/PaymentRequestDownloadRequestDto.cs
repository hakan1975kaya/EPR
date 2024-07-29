using Core.Entities.Abstract;
using Entities.Concrete.Entities;
using Entities.Concrete.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.Dtos.PaymentRequestDtos.PaymentRequestDownloadDtos
{
    public class PaymentRequestDownloadRequestDto : IDto
    {
        public string SftpFileName { get; set; }
        public int CorporateId { get; set; }
    }
}

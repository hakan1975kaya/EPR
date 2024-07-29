﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.Dtos.SmtpDtos.SmtpSuccessMailDtos
{
    public class SmtpSuccessMailResponseDto
    {
        public int CorporateCode { get; set; }
        public string CorporateName { get; set; }
        public string FileName { get; set; }
        public int Count { get; set; }
        public decimal TotalAmount { get; set; }
    }
}

﻿using Core.Entities.Abstract;
using Entities.Concrete.Enums;

namespace Entities.Concrete.Dtos.PaymentRequestSummaryDtos.PaymentRequestSummaryGetListByPaymentRequestIdDtos
{
    public class PaymentRequestSummaryGetListByPaymentRequestIdResponseDto : IDto
    {
        public int Id { get; set; }
        public int PaymentRequestId { get; set; }
        public StatusEnum Status { get; set; }
        public DateTime UploadDate { get; set; }
        public int Quantity { get; set; }
        public decimal Amount { get; set; }
        public int UserId { get; set; }
        public DateTime SystemEnteredDate { get; set; }
        public DateTime SystemEnteredTime { get; set; }
        public int SystemEnteredRegistrationNumber { get; set; }
        public DateTime Optime { get; set; }
        public bool IsActive { get; set; }
    }
}

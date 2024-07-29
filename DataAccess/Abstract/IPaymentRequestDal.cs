using Core.DataAccess.Abstract;
using Core.Entities.Concrete.Entities;
using Core.Utilities.Results.Abstract;
using Entities.Concrete.Dtos.PaymentRequestDtos.PaymentRequestGetByTodayDtos;
using Entities.Concrete.Dtos.PaymentRequestDtos.PaymentRequestSearchDtos;
using Entities.Concrete.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IPaymentRequestDal:IEntityRepository<PaymentRequest>
    {
        Task<List<PaymentRequestSearchResponseDto>> Search(PaymentRequestSearchRequestDto paymentRequestSearchRequestDto);
        Task<List<PaymentRequestGetByTodayResponseDto>> GetByToday();
    }
}

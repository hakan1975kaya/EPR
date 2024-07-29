using Core.DataAccess.Concrete.EntityFramework;
using Core.Entities.Concrete.Entities;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete.Dtos.PaymentRequestDtos.PaymentRequestGetByTodayDtos;
using Entities.Concrete.Dtos.PaymentRequestDtos.PaymentRequestSearchDtos;

namespace DataAccess.Concrete.EntityFramework
{
    public class EFPaymentRequestDal : EFEntityRepositoryBase<PaymentRequest, EPRContext>, IPaymentRequestDal
    {
        public async Task<List<PaymentRequestSearchResponseDto>> Search(PaymentRequestSearchRequestDto paymentRequestSearchRequestDto)
        {
            var filter = paymentRequestSearchRequestDto.Filter;

            using (var context = new EPRContext())
            {
                var result = from pr in context.PaymentRequests
                             join prd in context.PaymentRequestDetails on pr.Id equals prd.PaymentRequestId
                             join c in context.Corporates on pr.CorporateId equals c.Id
                             join u in context.Users on pr.UserId equals u.Id
                             where
                             pr.IsActive == true &&
                             prd.IsActive == true &&
                             c.IsActive == true &&
                             u.IsActive == true && (
                             pr.RequestNumber.ToLower().Contains(filter.ToLower()) ||
                             c.CorporateCode.ToString().ToLower().Contains(filter.ToLower()) ||
                             u.RegistrationNumber.ToString().ToLower().Contains(filter.ToLower()) ||
                             prd.ReferenceNumber.ToLower().Contains(filter.ToLower()) ||
                             prd.AccountNumber.ToString().ToLower().Contains(filter.ToLower()) ||
                             prd.CustomerNumber.ToString().ToLower().Contains(filter.ToLower()) ||
                             prd.FirstName.ToLower().Contains(filter.ToLower()) ||
                             prd.LastName.ToLower().Contains(filter.ToLower()) ||
                             prd.CardDepositDate.ToString().ToLower().Contains(filter.ToLower()))
                             select new PaymentRequestSearchResponseDto
                             {
                                 CorporateCode = c.CorporateCode,
                                 CorporateName = c.CorporateName,
                                 Id = pr.Id,
                                 IsActive = pr.IsActive,
                                 MoneyType = pr.MoneyType,
                                 Amount = (from prd in context.PaymentRequestDetails where prd.PaymentRequestId == pr.Id select prd).Sum(x => x.PaymentAmount),
                                 Quantity = (from prd in context.PaymentRequestDetails where prd.PaymentRequestId == pr.Id select prd).Count(),
                                 RegistrationNumber = u.RegistrationNumber,
                                 RequestNumber = pr.RequestNumber,
                                 Optime = pr.Optime,
                                 Status = pr.Status,
                             };

                return result.Distinct().OrderByDescending(x => x.Optime).ToList();
            }
        }

        public async Task<List<PaymentRequestGetByTodayResponseDto>> GetByToday()
        {

            using (var context = new EPRContext())
            {
                var result = from pr in context.PaymentRequests            
                             join c in context.Corporates on pr.CorporateId equals c.Id
                             join u in context.Users on pr.UserId equals u.Id
                             where
                             pr.IsActive == true &&
                             c.IsActive == true &&
                             u.IsActive == true &&
                             pr.Optime > DateTime.Today.AddDays(-1) &&
                             pr.Optime < DateTime.Today.AddDays(1)
                             select new PaymentRequestGetByTodayResponseDto
                             {
                                 CorporateCode = c.CorporateCode,
                                 CorporateName = c.CorporateName,
                                 Id = pr.Id,
                                 IsActive = pr.IsActive,
                                 MoneyType = pr.MoneyType,
                                 Amount = (from prd in context.PaymentRequestDetails where prd.PaymentRequestId == pr.Id select prd).Sum(x => x.PaymentAmount),
                                 Quantity = (from prd in context.PaymentRequestDetails where prd.PaymentRequestId == pr.Id select prd).Count(),
                                 RegistrationNumber = u.RegistrationNumber,
                                 RequestNumber = pr.RequestNumber,
                                 Optime = pr.Optime,
                                 Status = pr.Status,
                             };

                return result.Distinct().OrderByDescending(x => x.Optime).ToList();
            }
        }

    }
}

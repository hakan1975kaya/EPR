using Core.DataAccess.Concrete.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete.Dtos.PaymentRequestSummaryDtos.PaymentRequestSummaryAmountByCorporateIdYearDtos;
using Entities.Concrete.Dtos.PaymentRequestSummaryDtos.PaymentRequestSummaryChartByCorporateIdYear;
using Entities.Concrete.Dtos.PaymentRequestSummaryDtos.PaymentRequestSummaryGetByTodayDtos;
using Entities.Concrete.Dtos.PaymentRequestSummaryDtos.PaymentRequestSummarySearchDtos;
using Entities.Concrete.Dtos.PaymentRequestSummaryDtos.PaymentRequestTotalOutgoingPaymentDtos;
using Entities.Concrete.Entities;
using Entities.Concrete.Enums;

namespace DataAccess.Concrete.EntityFramework
{
    public class EFPaymentRequestSummaryDal : EFEntityRepositoryBase<PaymentRequestSummary, EPRContext>, IPaymentRequestSummaryDal
    {
        public async Task<List<PaymentRequestSummaryAmountByCorporateIdYearResponseDto>> AmountByCorporateIdYear(PaymentRequestSummaryAmountByCorporateIdYearRequestDto paymentRequestSummaryAmountByCorporateIdYearRequestDto)
        {
            var corporateId = paymentRequestSummaryAmountByCorporateIdYearRequestDto.CorporateId;
            var year = paymentRequestSummaryAmountByCorporateIdYearRequestDto.Year;
            using (var context = new EPRContext())
            {
                var result = from prs in context.PaymentRequestSummaries
                             join pr in context.PaymentRequests on prs.PaymentRequestId equals pr.Id
                             join c in context.Corporates on pr.CorporateId equals c.Id
                             join sd in context.SftpDownloads on pr.FileId equals sd.Id
                             where prs.IsActive == true &&
                             pr.IsActive == true &&
                             c.IsActive == true &&
                             sd.IsActive == true &&
                             c.Id == corporateId &&
                             prs.SystemEnteredDate.Year == year &&
                             prs.Status == StatusEnum.SummaryApproved
                             orderby prs.SystemEnteredDate.Month
                             group prs by prs.SystemEnteredDate.Month.ToString() into g
                             select new PaymentRequestSummaryAmountByCorporateIdYearResponseDto
                             {
                                 Name = g.Key,
                                 Value = g.Sum(x => x.Amount)

                             };

                return result.ToList();
            }
        }

        public async Task<List<PaymentRequestSummaryGetByTodayResponseDto>> GetByToday()
        {
            using (var context = new EPRContext())
            {
                var result = from pr in context.PaymentRequests
                             join c in context.Corporates on pr.CorporateId equals c.Id
                             join u in context.Users on pr.UserId equals u.Id
                             join prs in context.PaymentRequestSummaries on pr.Id equals prs.PaymentRequestId into paymentRequestSummariesDefault
                             from prsd in paymentRequestSummariesDefault.DefaultIfEmpty()
                             where
                             pr.IsActive == true &&
                             c.IsActive == true &&
                             u.IsActive == true &&
                             pr.Optime > DateTime.Today.AddDays(-1) &&
                             pr.Optime < DateTime.Today.AddDays(1)
                             select new PaymentRequestSummaryGetByTodayResponseDto
                             {
                                 Id = prsd.Id != null ? prsd.Id : 0,
                                 PaymentRequestId = pr.Id,
                                 RequestNumber = pr.RequestNumber,
                                 CorporateCode = c.CorporateCode,
                                 CorporateName = c.CorporateName,
                                 Status = prsd.Status != null ? prsd.Status  : StatusEnum.SummaryWaitingForApproval,
                                 UploadDate = prsd.UploadDate != null ? prsd.UploadDate : DateTime.Now,
                                 Quantity = prsd.Quantity != null ? prsd.Quantity : 0,
                                 Amount = prsd.Amount != null ? prsd.Amount : 0,
                                 RegistrationNumber = u.RegistrationNumber,
                                 SystemEnteredDate = prsd.SystemEnteredDate != null ? prsd.SystemEnteredDate : DateTime.Now,
                                 SystemEnteredTime = prsd.SystemEnteredTime != null ? prsd.SystemEnteredTime : TimeSpan.FromHours(DateTime.Now.Hour),
                                 SystemEnteredRegistrationNumber = prsd.SystemEnteredRegistrationNumber != null ? prsd.SystemEnteredRegistrationNumber : 0,
                                 IsActive = prsd.IsActive != null ? prsd.IsActive : false,
                             };

                return result.Distinct().OrderByDescending(x => x.SystemEnteredDate).ToList();
            }
        }


        public async Task<List<PaymentRequestSummarySearchResponseDto>> Search(PaymentRequestSummarySearchRequestDto paymentRequestSummarySearchRequestDto)
        {
            var filter = paymentRequestSummarySearchRequestDto.Filter;

            using (var context = new EPRContext())
            {
                var result = from pr in context.PaymentRequests
                             join c in context.Corporates on pr.CorporateId equals c.Id
                             join u in context.Users on pr.UserId equals u.Id
                             join prs in context.PaymentRequestSummaries on pr.Id equals prs.PaymentRequestId into paymentRequestSummariesDefault
                             from prsd in paymentRequestSummariesDefault.DefaultIfEmpty()
                             where
                             pr.IsActive == true &&
                             c.IsActive == true &&
                             u.IsActive == true && (
                             c.CorporateCode.ToString().ToLower().Contains(filter.ToLower()) ||
                             c.CorporateName.ToLower().Contains(filter.ToLower()) ||
                             pr.RequestNumber.ToLower().Contains(filter.ToLower()))
                             select new PaymentRequestSummarySearchResponseDto
                             {
                                 Id = prsd.Id != null ? prsd.Id : 0,
                                 PaymentRequestId = pr.Id,
                                 RequestNumber = pr.RequestNumber,
                                 CorporateCode = c.CorporateCode,
                                 CorporateName = c.CorporateName,
                                 Status = prsd.Status != null ? prsd.Status  : StatusEnum.SummaryWaitingForApproval,
                                 UploadDate = prsd.UploadDate != null ? prsd.UploadDate : DateTime.Now,
                                 Quantity = prsd.Quantity != null ? prsd.Quantity : 0,
                                 Amount = prsd.Amount != null ? prsd.Amount : 0,
                                 RegistrationNumber = u.RegistrationNumber,
                                 SystemEnteredDate = prsd.SystemEnteredDate != null ? prsd.SystemEnteredDate : DateTime.Now,
                                 SystemEnteredTime = prsd.SystemEnteredTime != null ? prsd.SystemEnteredTime : TimeSpan.FromHours(DateTime.Now.Hour),
                                 SystemEnteredRegistrationNumber = prsd.SystemEnteredRegistrationNumber != null ? prsd.SystemEnteredRegistrationNumber : 0,
                                 IsActive = prsd.IsActive != null ? prsd.IsActive : false,
                             };

                return result.Distinct().OrderByDescending(x => x.SystemEnteredDate).ToList();
            }
        }


        public async Task<List<PaymentRequestSummaryTotalOutgoingPaymentResponseDto>> TotalOutgoingPayment()
        {
            using (var context = new EPRContext())
            {
                var result = from pr in context.PaymentRequests
                             join prd in context.PaymentRequestDetails on pr.Id equals prd.PaymentRequestId
                             where pr.IsActive == true &&
                             prd.IsActive == true &&
                             pr.Optime.Year == DateTime.Now.Year
                             orderby pr.Status
                             group new { pr, prd } by pr.Status into g
                             select new PaymentRequestSummaryTotalOutgoingPaymentResponseDto
                             {
                                 Name = g.Key,
                                 Value = g.Sum(x => x.prd.PaymentAmount)

                             };

                return result.ToList();
            }
        }












    }
}
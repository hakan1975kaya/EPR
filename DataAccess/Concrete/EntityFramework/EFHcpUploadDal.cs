using Core.DataAccess.Concrete.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete.Dtos.HcpUploadDtos.HcpUploadSearchDtos;
using Entities.Concrete.Entities;

namespace DataAccess.Concrete.EntityFramework
{
    public class EFHcpUploadDal : EFEntityRepositoryBase<HcpUpload, EPRContext>, IHcpUploadDal
    {
        public async Task<List<HcpUploadSearchResponseDto>> Search(HcpUploadSearchRequestDto hcpUploadSearchRequestDto)
        {
            var corporateId = hcpUploadSearchRequestDto.CorporateId;
            var requestNumber = hcpUploadSearchRequestDto.RequestNumber;
            var startDate = hcpUploadSearchRequestDto.StartDate;
            var endDate = hcpUploadSearchRequestDto.EndDate;
            using (var context = new EPRContext())
            {
                var result = from hu in context.HcpUploads
                             join pr in context.PaymentRequests on hu.PaymentRequestId equals pr.Id
                             join c in context.Corporates on pr.CorporateId equals c.Id
                             join u in context.Users on pr.UserId equals u.Id
                             join prs in context.PaymentRequestSummaries on pr.Id equals prs.PaymentRequestId
                             where
                             hu.IsActive == true &&
                             pr.IsActive == true &&
                             c.IsActive == true &&
                             prs.IsActive == true &&
                             c.Id == corporateId &&
                             (hu.Optime >= startDate || startDate== null) &&
                             (hu.Optime <= endDate || endDate == null) &&
                             (pr.RequestNumber == requestNumber || requestNumber == null)
                             select new HcpUploadSearchResponseDto
                             {
                                 CorporateCode = c.CorporateCode,
                                 CorporateName = c.CorporateName,
                                 HcpId = hu.HcpId,
                                 Id = hu.Id,
                                 MoneyType = pr.MoneyType,
                                 Optime = hu.Optime,
                                 RegistrationNumber = u.RegistrationNumber,
                                 RequestNumber = pr.RequestNumber,
                                 Status = pr.Status,
                                 Amount = prs.Amount,
                                 Quantity = prs.Quantity,
                                 Explanation = hu.Explanation,
                                 Extension = hu.Extension,
                             };

                return result.Distinct().OrderBy(x => x.CorporateCode).ToList();
            }
        }
    }
}

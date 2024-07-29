using Core.DataAccess.Concrete.EntityFramework;
using Core.Entities.Concrete.Entities;
using Core.Utilities.Results.Abstract;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete.Dtos.CorporateMailAddressDtos.CorporateMailAddressGetListByCorporateIdDtos;
using Entities.Concrete.Dtos.CorporateMailAddressDtos.CorporateMailAddressSearchDtos;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EFCorporateMailAddressDal : EFEntityRepositoryBase<CorporateMailAddress, EPRContext>, ICorporateMailAddressDal
    {
        public async Task<List<CorporateMailAddressGetListByCorporateIdResponseDto>> GetByCorporateId(int corporateId)
        {
            using (var context = new EPRContext())
            {
                var result = new List<CorporateMailAddressGetListByCorporateIdResponseDto>();

                var resultCorporates = (from cma in context.CorporateMailAddresses
                             join c in context.Corporates on cma.CorporateId equals c.Id
                             join ma in context.MailAddresses on cma.MailAddressId equals ma.Id
                             where
                             cma.IsActive == true &&
                             c.IsActive == true &&
                             ma.IsActive == true && 
                             cma.CorporateId==corporateId
                             select new CorporateMailAddressGetListByCorporateIdResponseDto
                             {
                                 Address = ma.Address,
                                 CorporateCode = c.CorporateCode,
                                 CorporateId = c.Id,
                                 CorporateName = c.CorporateName,
                                 Id = cma.Id,
                                 IsActive = cma.IsActive,
                                 MailAddressId = ma.Id,
                                 Optime = cma.Optime
                             }).ToList();

                var resultPtts = (from  ma in context.MailAddresses 
                                      where
                                      ma.IsActive == true &&
                                      ma.IsPtt==true 
                                      select new CorporateMailAddressGetListByCorporateIdResponseDto
                                      {
                                          Address = ma.Address,
                                          CorporateCode = 0,
                                          CorporateId = 0,
                                          CorporateName = "",
                                          Id = 0,
                                          IsActive =true,
                                          MailAddressId = ma.Id,
                                          Optime =DateTime.Now
                                      }).ToList();

                if(resultCorporates!=null)
                {
                    if(resultCorporates.Count()>0)
                    {
                        resultCorporates.ForEach(resultCorporate =>
                        {
                            result.Add(resultCorporate);
                        });
                    }
                }

                if(resultPtts!=null)
                {
                    if(resultPtts.Count()>0) 
                    {
                        resultPtts.ForEach(resultPtt => {
                            result.Add(resultPtt);
                        });
                    }
                }
            

                return result.Distinct().OrderBy(x => x.Address).ToList();
            }
        }

        public async Task<List<CorporateMailAddressSearchResponseDto>> Search(CorporateMailAddressSearchRequestDto corporateMailAddressSearchRequestDto)
        {
            var filter = corporateMailAddressSearchRequestDto.Filter;
            using (var context = new EPRContext())
            {
                var result = from cma in context.CorporateMailAddresses
                             join c in context.Corporates on cma.CorporateId equals c.Id
                             join ma in context.MailAddresses on cma.MailAddressId equals ma.Id
                             where
                             cma.IsActive == true &&
                             c.IsActive == true &&
                             ma.IsActive == true &&
                             (ma.Address.ToLower().Contains(filter.ToLower()) ||
                             c.CorporateCode.ToString().ToLower().Contains(filter.ToLower()) ||
                             c.CorporateName.ToLower().Contains(filter.ToLower())
                             )
                             select new CorporateMailAddressSearchResponseDto
                             {
                                 Address = ma.Address,
                                 CorporateCode = c.CorporateCode,
                                 CorporateId = c.Id,
                                 CorporateName = c.CorporateName,
                                 Id = cma.Id,
                                 IsActive = cma.IsActive,
                                 MailAddressId = ma.Id,
                                 Optime = cma.Optime
                             };

                return result.Distinct().OrderBy(x => x.Address).ToList();
            }
        }
    }
}
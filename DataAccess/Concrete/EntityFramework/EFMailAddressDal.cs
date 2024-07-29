using Core.DataAccess.Concrete.EntityFramework;
using Core.Entities.Concrete.Entities;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete.Dtos.MailAddressDtos.MailAddressSearchDtos;

namespace DataAccess.Concrete.EntityFramework
{
    public class EFMailAddressDal : EFEntityRepositoryBase<MailAddress, EPRContext>, IMailAddressDal
    {
        public async Task<List<MailAddressSearchResponseDto>> Search(MailAddressSearchRequestDto mailAddressSearchRequestDto)
        {
            var filter = mailAddressSearchRequestDto.Filter;
            using (var context = new EPRContext())
            {
                var result = from cma in context.MailAddresses
                             where cma.IsActive == true && cma.Address.ToLower().Contains(filter.ToLower())
                             select new MailAddressSearchResponseDto
                             {
                                 Id = cma.Id,
                                 Address = cma.Address,
                                 IsActive = cma.IsActive,
                                 IsCC = cma.IsCC,
                                 IsPtt = cma.IsPtt,
                                 Optime = cma.Optime,
                             };

                return result.Distinct().OrderBy(x => x.Address).ToList();
            }
        }
    }
}
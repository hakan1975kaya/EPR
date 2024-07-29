using AutoMapper;
using Core.Entities.Concrete.Entities;
using Entities.Concrete.Dtos.CorporateMailAddressDtos.CorporateMailAddressAddDtos;
using Entities.Concrete.Dtos.CorporateMailAddressDtos.CorporateMailAddressGetByIdDtos;
using Entities.Concrete.Dtos.CorporateMailAddressDtos.CorporateMailAddressGetListByCorporateIdDtos;
using Entities.Concrete.Dtos.CorporateMailAddressDtos.CorporateMailAddressGetListDtos;
using Entities.Concrete.Dtos.CorporateMailAddressDtos.CorporateMailAddressUpdateDtos;

namespace Entities.Utilities.MappingProfiles
{
    public class CorporateMailAddressMappingProfile:Profile
    {
        public CorporateMailAddressMappingProfile()
        {
            CreateMap<CorporateMailAddress, CorporateMailAddressAddRequestDto>();
            CreateMap<CorporateMailAddressAddRequestDto, CorporateMailAddress>();

            CreateMap<CorporateMailAddress, CorporateMailAddressGetByIdResponseDto>();
            CreateMap<CorporateMailAddressGetByIdResponseDto, CorporateMailAddress>();

            CreateMap<CorporateMailAddress, CorporateMailAddressGetListResponseDto>();
            CreateMap<CorporateMailAddressGetListResponseDto, CorporateMailAddress>();

            CreateMap<CorporateMailAddress, CorporateMailAddressUpdateRequestDto>();
            CreateMap<CorporateMailAddressUpdateRequestDto, CorporateMailAddress>();

            CreateMap<CorporateMailAddress, CorporateMailAddressGetListByCorporateIdResponseDto>();
            CreateMap<CorporateMailAddressGetListByCorporateIdResponseDto, CorporateMailAddress>();
        }
    }
}

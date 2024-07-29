using AutoMapper;
using Core.Aspects.Autofac.Performance.Dtos;
using Core.Entities.Concrete.Entities;
using Entities.Concrete.Dtos.MailAddressDtos.MailAddressAddDtos;
using Entities.Concrete.Dtos.MailAddressDtos.MailAddressGetByIdDtos;
using Entities.Concrete.Dtos.MailAddressDtos.MailAddressGetListDtos;
using Entities.Concrete.Dtos.MailAddressDtos.MailAddressSearchDtos;
using Entities.Concrete.Dtos.MailAddressDtos.MailAddressUpdateDtos;

namespace Entities.Utilities.MappingProfiles
{
    public class MailAddressMappingProfile:Profile
    {
        public MailAddressMappingProfile()
        {
            CreateMap<MailAddress, MailAddressAddRequestDto>();
            CreateMap<MailAddressAddRequestDto, MailAddress>();

            CreateMap<MailAddress, MailAddressGetByIdResponseDto>();
            CreateMap<MailAddressGetByIdResponseDto, MailAddress>();

            CreateMap<MailAddress, MailAddressGetListResponseDto>();
            CreateMap<MailAddressGetListResponseDto, MailAddress>();

            CreateMap<MailAddress, MailAddressUpdateRequestDto>();
            CreateMap<MailAddressUpdateRequestDto, MailAddress>();

            CreateMap<MailAddress, MailAddressSearchResponseDto>();
            CreateMap<MailAddressSearchResponseDto, MailAddress>();

            CreateMap<MailAddress, MailAddressGetListPttResposeDto>();
            CreateMap<MailAddressGetListPttResposeDto, MailAddress>();

            CreateMap<MailAddress, MailAddressGetListNotPttResposeDto>();
            CreateMap<MailAddressGetListNotPttResposeDto, MailAddress>();

        }
    }
}

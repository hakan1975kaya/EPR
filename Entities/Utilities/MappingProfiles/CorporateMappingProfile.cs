using AutoMapper;
using Entities.Concrete.Dtos.CorporateDtos.CorporateAddDtos;
using Entities.Concrete.Dtos.CorporateDtos.CorporateGetByCorporateCodeDtos;
using Entities.Concrete.Dtos.CorporateDtos.CorporateGetByIdDtos;
using Entities.Concrete.Dtos.CorporateDtos.CorporateGetListDtos;
using Entities.Concrete.Dtos.CorporateDtos.CorporateGetListPrefixAvailableDtos;
using Entities.Concrete.Dtos.CorporateDtos.CorporateSearchDtos;
using Entities.Concrete.Dtos.CorporateDtos.CorporateUpdateDtos;
using Entities.Concrete.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Utilities.MappingProfiles
{
    public class CorporateMappingProfile:Profile
    {
        public CorporateMappingProfile()
        {
            CreateMap<Corporate,CorporateAddRequestDto>();
            CreateMap<CorporateAddRequestDto, Corporate>();

            CreateMap<Corporate, CorporateUpdateRequestDto>();
            CreateMap<CorporateUpdateRequestDto, Corporate>();

            CreateMap<Corporate, CorporateGetByIdResponseDto>();
            CreateMap<CorporateGetByIdResponseDto, Corporate>();

            CreateMap<Corporate, CorporateGetListResponseDto>();
            CreateMap<CorporateGetListResponseDto, Corporate>();

            CreateMap<Corporate, CorporateSearchResponseDto>();
            CreateMap<CorporateSearchResponseDto, Corporate>();

            CreateMap<Corporate, CorporateGetListPrefixAvailableResponseDto>();
            CreateMap<CorporateGetListPrefixAvailableResponseDto, Corporate>();

            CreateMap<Corporate, CorporateGetByCorporateCodeResponseDto>();
            CreateMap<CorporateGetByCorporateCodeResponseDto, Corporate>();
        }
    }
}

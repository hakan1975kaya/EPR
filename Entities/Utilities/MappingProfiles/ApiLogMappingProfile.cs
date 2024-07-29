using AutoMapper;
using Entities.Concrete.Dtos.ApiLogDtos.ApiLogAddDtos;
using Entities.Concrete.Dtos.ApiLogDtos.ApiLogGetByldDtos;
using Entities.Concrete.Dtos.ApiLogDtos.ApiLogGetListDtos;
using Entities.Concrete.Dtos.ApiLogDtos.ApiLogUpdateDtos;
using Entities.Concrete.Entities;

namespace Entities.Utilities.MappingProfiles
{
    public class ApiLogMappingProfile : Profile
    {
        public ApiLogMappingProfile()
        {
            CreateMap<ApiLog, ApiLogAddRequestDto>();
            CreateMap<ApiLogAddRequestDto, ApiLog>();

            CreateMap<ApiLog, ApiLogUpdateRequestDto>();
            CreateMap<ApiLogUpdateRequestDto, ApiLog>();

            CreateMap<ApiLog, ApiLogGetByIdResponseDto>();
            CreateMap<ApiLogGetByIdResponseDto, ApiLog>();

            CreateMap<ApiLog, ApiLogGetListResponseDto>();
            CreateMap<ApiLogGetListResponseDto, ApiLog>();
        }
    }
}

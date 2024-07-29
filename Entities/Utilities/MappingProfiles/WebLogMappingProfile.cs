using AutoMapper;
using Entities.Concrete.Dtos.WebLogDtos.WebLogAddDtos;
using Entities.Concrete.Dtos.WebLogDtos.WebLogGetByIdDtos;
using Entities.Concrete.Dtos.WebLogDtos.WebLogGetListDtos;
using Entities.Concrete.Dtos.WebLogDtos.WebLogUpdateDtos;
using Entities.Concrete.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Utilities.MappingProfiles
{
    public class WebLogMappingProfile : Profile
    {
        public WebLogMappingProfile()
        {
            CreateMap<WebLog, WebLogAddRequestDto>();
            CreateMap<WebLogAddRequestDto, WebLog>();

            CreateMap<WebLog, WebLogUpdateRequestDto>();
            CreateMap<WebLogUpdateRequestDto, WebLog>();

            CreateMap<WebLog, WebLogGetListResponseDto>();
            CreateMap<WebLogGetListResponseDto, WebLog>();

            CreateMap<WebLog, WebLogGetByIdResponseDto>();
            CreateMap<WebLogGetByIdResponseDto, WebLog>();
        }
    }
}

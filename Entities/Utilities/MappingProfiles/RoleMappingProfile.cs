using AutoMapper;
using Core.Entities.Concrete.Entities;
using Entities.Concrete.Dtos.RoleDtos.RoleAddDtos;
using Entities.Concrete.Dtos.RoleDtos.RoleGetByIdDtos;
using Entities.Concrete.Dtos.RoleDtos.RoleGetListDtos;
using Entities.Concrete.Dtos.RoleDtos.RoleSearchDtos;
using Entities.Concrete.Dtos.RoleDtos.RoleUpdateDtos;
using Entities.Concrete.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Utilities.MappingProfiles
{
    public class RoleMappingProfile:Profile
    {
        public RoleMappingProfile()
        {
            CreateMap<Role,RoleAddRequestDto>();
            CreateMap<RoleAddRequestDto, Role>();

            CreateMap<Role, RoleUpdateRequestDto>();
            CreateMap<RoleUpdateRequestDto, Role>();

            CreateMap<Role, RoleGetByIdResponseDto>();
            CreateMap<RoleGetByIdResponseDto, Role>();

            CreateMap<Role, RoleGetListResponseDto>();
            CreateMap<RoleGetListResponseDto, Role>();

            CreateMap<Role, RoleSearchResponseDto>();
            CreateMap<RoleSearchResponseDto, Role>();
        }
    }
}

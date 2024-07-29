using AutoMapper;
using Entities.Concrete.Dtos.UserRoleDtos.UserRoleAddDtos;
using Entities.Concrete.Dtos.UserRoleDtos.UserRoleGetByIdDtos;
using Entities.Concrete.Dtos.UserRoleDtos.UserRoleGetListByCorporateIdDtos;
using Entities.Concrete.Dtos.UserRoleDtos.UserRoleGetListDtos;
using Entities.Concrete.Dtos.UserRoleDtos.UserRoleUpdateDtos;
using Entities.Concrete.Entities;

namespace Entities.Utilities.MappingProfiles
{
    public class UserRoleMappingProfile:Profile
    {
        public UserRoleMappingProfile()
        {
            CreateMap<UserRole, UserRoleAddRequestDto>();
            CreateMap<UserRoleAddRequestDto, UserRole>();

            CreateMap<UserRole, UserRoleGetByIdResponseDto>();
            CreateMap<UserRoleGetByIdResponseDto, UserRole>();

            CreateMap<UserRole, UserRoleGetListResponseDto>();
            CreateMap<UserRoleGetListResponseDto, UserRole>();

            CreateMap<UserRole, UserRoleUpdateRequestDto>();
            CreateMap<UserRoleUpdateRequestDto, UserRole>();

            CreateMap<UserRole, UserRoleGetListByRoleIdResponseDto>();
            CreateMap<UserRoleGetListByRoleIdResponseDto, UserRole>();
        }
    }
}

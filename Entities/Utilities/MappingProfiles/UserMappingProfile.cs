using AutoMapper;
using Core.Entities.Concrete.Entities;
using Entities.Concrete.Dtos.AuthDtos.AccessTokenDtoS;
using Entities.Concrete.Dtos.AuthDtos.RegisterDtos;
using Entities.Concrete.Dtos.UserDtos.UserAddDtos;
using Entities.Concrete.Dtos.UserDtos.UserGetByIdDtos;
using Entities.Concrete.Dtos.UserDtos.UserGetListDtos;
using Entities.Concrete.Dtos.UserDtos.UserSaveDtos;
using Entities.Concrete.Dtos.UserDtos.UserSearchDtos;
using Entities.Concrete.Dtos.UserDtos.UserUpdateDtos;

namespace Entities.Utilities.MappingProfiles
{
    public class UserMappingProfile:Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User, UserAddRequestDto>();
            CreateMap<UserAddRequestDto, User>();

            CreateMap<User, UserUpdateRequestDto>();
            CreateMap<UserUpdateRequestDto, User>();

            CreateMap<User, RegisterRequestDto>();
            CreateMap<RegisterRequestDto, User>();

            CreateMap<User, AccessTokenAddRequestDto>();
            CreateMap<AccessTokenAddRequestDto, User>();

            CreateMap<User, UserGetByIdResponseDto>();
            CreateMap<UserGetByIdResponseDto, User>();

            CreateMap<User, UserGetListResponseDto>();
            CreateMap<UserGetListResponseDto, User>();

            CreateMap<User, UserSearchResponseDto>();
            CreateMap<UserSearchResponseDto, User>();

            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
        }
    }
}

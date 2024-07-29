using AutoMapper;
using Core.Entities.Concrete.Entities;
using Entities.Concrete.Dtos.UserDtos.UserGetByRegistrationNumberDtos;

namespace Entities.Utilities.MappingProfiles
{
    public class AuthMappingProfile : Profile
    {
        public AuthMappingProfile()
        {
            CreateMap<User, UserGetByRegistrationNumberResponseDto>();
            CreateMap<UserGetByRegistrationNumberResponseDto, User>();
        }
    }
}

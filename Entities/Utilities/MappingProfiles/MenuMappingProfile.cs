using AutoMapper;
using Entities.Concrete.Dtos.MenuDtos.MenuAddDtos;
using Entities.Concrete.Dtos.MenuDtos.MenuGetByIdDtos;
using Entities.Concrete.Dtos.MenuDtos.MenuGetListDtos;
using Entities.Concrete.Dtos.MenuDtos.MenuSearchDtos;
using Entities.Concrete.Dtos.MenuDtos.MenuUpdateDtos;
using Entities.Concrete.Entities;

namespace Entities.Utilities.MappingProfiles
{
    public class MenuMappingProfile:Profile
    {
        public MenuMappingProfile()
        {
            CreateMap<Menu,MenuAddRequestDto>();
            CreateMap<MenuAddRequestDto, Menu>();

            CreateMap<Menu, MenuUpdateRequestDto>();
            CreateMap<MenuUpdateRequestDto, Menu>();

            CreateMap<Menu, MenuGetByIdResponseDto>();
            CreateMap<MenuGetByIdResponseDto, Menu>();

            CreateMap<Menu, MenuGetListResponseDto>();
            CreateMap<MenuGetListResponseDto, Menu>();

            CreateMap<Menu, MenuSearchResponseDto>();
            CreateMap<MenuSearchResponseDto, Menu>();
        }
    }
}

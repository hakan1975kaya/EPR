using AutoMapper;
using Entities.Concrete.Dtos.MenuDtos.MenuSearchDtos;
using Entities.Concrete.Dtos.MenuOperationClaimDtos.MenuOperationClaimAddDtos;
using Entities.Concrete.Dtos.MenuOperationClaimDtos.MenuOperationClaimGetByIdDtos;
using Entities.Concrete.Dtos.MenuOperationClaimDtos.MenuOperationClaimGetListDtos;
using Entities.Concrete.Dtos.MenuOperationClaimDtos.MenuOperationClaimUpdateDtos;
using Entities.Concrete.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Utilities.MappingProfiles
{
    public class MenuOperationClaimMappingProfile:Profile
    {
        public MenuOperationClaimMappingProfile()
        {
            CreateMap<MenuOperationClaim,MenuOperationClaimAddRequestDto>();
            CreateMap<MenuOperationClaimAddRequestDto, MenuOperationClaim>();

            CreateMap<MenuOperationClaim, MenuOperationClaimUpdateRequestDto>();
            CreateMap<MenuOperationClaimUpdateRequestDto, MenuOperationClaim>();

            CreateMap<MenuOperationClaim, MenuOperationClaimGetByIdResponseDto>();
            CreateMap<MenuOperationClaimGetByIdResponseDto, MenuOperationClaim>();

            CreateMap<MenuOperationClaim, MenuOperationClaimGetListResponseDto>();
            CreateMap<MenuOperationClaimGetListResponseDto, MenuOperationClaim>();
        }
    }
}


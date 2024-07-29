using AutoMapper;
using Core.Entities.Concrete.Entities;
using Entities.Concrete.Dtos.RoleOperationClaimDtos.RoleOperationClaimAddDtos;
using Entities.Concrete.Dtos.RoleOperationClaimDtos.RoleOperationClaimGetByIdDtos;
using Entities.Concrete.Dtos.RoleOperationClaimDtos.RoleOperationClaimGetListDtos;
using Entities.Concrete.Dtos.RoleOperationClaimDtos.RoleOperationClaimUpdateDtos;

namespace Entities.Utilities.MappingProfiles
{
    public class RoleOperationClaimMappingProfile:Profile
    {
        public RoleOperationClaimMappingProfile()
        {
            CreateMap<RoleOperationClaim, RoleOperationClaimAddRequestDto>();
            CreateMap<RoleOperationClaimAddRequestDto, RoleOperationClaim>();

            CreateMap<RoleOperationClaim, RoleOperationClaimGetByIdResponseDto>();
            CreateMap<RoleOperationClaimGetByIdResponseDto, RoleOperationClaim>();

            CreateMap<RoleOperationClaim, RoleOperationClaimGetListResponseDto>();
            CreateMap<RoleOperationClaimGetListResponseDto, RoleOperationClaim>();

            CreateMap<RoleOperationClaim, RoleOperationClaimUpdateRequestDto>();
            CreateMap<RoleOperationClaimUpdateRequestDto, RoleOperationClaim>();          
        }
    }
}

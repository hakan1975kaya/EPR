using AutoMapper;
using Core.Entities.Concrete.Entities;
using Entities.Concrete.Dtos.OperationClaimDtos.OperationClaimAddDtos;
using Entities.Concrete.Dtos.OperationClaimDtos.OperationClaimChildListGetByUserIdResponseDtos;
using Entities.Concrete.Dtos.OperationClaimDtos.OperationClaimChildListResponseDtos;
using Entities.Concrete.Dtos.OperationClaimDtos.OperationClaimGetByIdDtos;
using Entities.Concrete.Dtos.OperationClaimDtos.OperationClaimGetListDtos;
using Entities.Concrete.Dtos.OperationClaimDtos.OperationClaimParentListGetByUserIdResponseDtos;
using Entities.Concrete.Dtos.OperationClaimDtos.OperationClaimParentListResponseDtos;
using Entities.Concrete.Dtos.OperationClaimDtos.OperationClaimSearchDtos;
using Entities.Concrete.Dtos.OperationClaimDtos.OperationClaimUpdateDtos;


namespace Entities.Utilities.MappingProfiles
{
    public class OperationClaimMappingProfile:Profile
    {
        public OperationClaimMappingProfile()
        {
            CreateMap<OperationClaim, OperationClaimAddRequestDto>();
            CreateMap<OperationClaimAddRequestDto, OperationClaim>();

            CreateMap<OperationClaim, OperationClaimGetByIdResponseDto>();
            CreateMap<OperationClaimGetByIdResponseDto, OperationClaim>();

            CreateMap<OperationClaim, OperationClaimGetListResponseDto>();
            CreateMap<OperationClaimGetListResponseDto, OperationClaim>();

            CreateMap<OperationClaim, OperationClaimUpdateRequestDto>();
            CreateMap<OperationClaimUpdateRequestDto, OperationClaim>();

            CreateMap<OperationClaim, OperationClaimSearchResponseDto>();
            CreateMap<OperationClaimSearchResponseDto, OperationClaim>();

            CreateMap<OperationClaim, OperationClaimParentListGetByUserIdResponseDto>();
            CreateMap<OperationClaimParentListGetByUserIdResponseDto, OperationClaim>();

            CreateMap<OperationClaim, OperationClaimChildListGetByUserIdResponseDto>();
            CreateMap<OperationClaimChildListGetByUserIdResponseDto, OperationClaim>();

            CreateMap<OperationClaim, OperationClaimParentListResponseDto>();
            CreateMap<OperationClaimParentListResponseDto, OperationClaim>();

            CreateMap<OperationClaim, OperationClaimChildListResponseDto>();
            CreateMap<OperationClaimChildListResponseDto, OperationClaim>();
        }
    }
}

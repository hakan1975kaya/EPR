
using AutoMapper;
using Entities.Concrete.Dtos.HcpUploadDtos.HcpUploadAddDtos;
using Entities.Concrete.Dtos.HcpUploadDtos.HcpUploadGetByldDtos;
using Entities.Concrete.Dtos.HcpUploadDtos.HcpUploadGetListDtos;
using Entities.Concrete.Dtos.HcpUploadDtos.HcpUploadUpdateDtos;
using Entities.Concrete.Entities;

namespace Entities.Utilities.MappingProfiles
{
    public class HcpUploadMappingProfile:Profile
    {
        public HcpUploadMappingProfile()
        {
            CreateMap<HcpUpload, HcpUploadAddRequestDto>();
            CreateMap<HcpUploadAddRequestDto, HcpUpload>();

            CreateMap<HcpUpload, HcpUploadUpdateRequestDto>();
            CreateMap<HcpUploadUpdateRequestDto, HcpUpload>();

            CreateMap<HcpUpload, HcpUploadGetByIdResponseDto>();
            CreateMap<HcpUploadGetByIdResponseDto, HcpUpload>();

            CreateMap<HcpUpload, HcpUploadGetListResponseDto>();
            CreateMap<HcpUploadGetListResponseDto, HcpUpload>();

        }
    }
}

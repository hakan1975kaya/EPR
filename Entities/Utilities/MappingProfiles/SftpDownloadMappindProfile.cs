using AutoMapper;
using Entities.Concrete.Dtos.SftpDownloadDtos.SftpDownloadAddDtos;
using Entities.Concrete.Dtos.SftpDownloadDtos.SftpDownloadGetListDtos;
using Entities.Concrete.Dtos.SftpDownloadDtos.SftpDownloadSearchDtos;
using Entities.Concrete.Dtos.SftpDownloadDtos.SftpDownloadUpdateDtos;
using Entities.Concrete.Dtos.SftpDownloadDtos.SftpDownloadGetByldDtos;

using Entities.Concrete.Entities;
using Entities.Concrete.Dtos.SftpDownloadDtos.SftpDownloadGetBySftpFileNameDtos;

namespace Entities.Utilities.MappingProfiles
{
    public class SftpDownloadMappingProfile : Profile
    {
        public SftpDownloadMappingProfile()
        {
            CreateMap<SftpDownload, SftpDownloadAddRequestDto>();
            CreateMap<SftpDownloadAddRequestDto, SftpDownload>();

            CreateMap<SftpDownload, SftpDownloadUpdateRequestDto>();
            CreateMap<SftpDownloadUpdateRequestDto, SftpDownload>();

            CreateMap<SftpDownload, SftpDownloadGetByIdResponseDto>();
            CreateMap<SftpDownloadGetByIdResponseDto, SftpDownload>();

            CreateMap<SftpDownload, SftpDownloadGetListResponseDto>();
            CreateMap<SftpDownloadGetListResponseDto, SftpDownload>();

            CreateMap<SftpDownload, SftpDownloadSearchResponseDto>();
            CreateMap<SftpDownloadSearchResponseDto, SftpDownload>();

            CreateMap<SftpDownload, SftpDownloadSearchResponseDto>();
            CreateMap<SftpDownloadSearchResponseDto, SftpDownload>();

            CreateMap<SftpDownload, SftpDownloadGetBySftpFileNameResponseDto>();
            CreateMap<SftpDownloadGetBySftpFileNameResponseDto, SftpDownload>();


        }
    }
}

using Core.Entities.Abstract;
using Entities.Concrete.Enums;

namespace Entities.Concrete.Dtos.SftpDownloadDtos.SftpDownloadSearchDtos
{
    public class SftpDownloadSearchRequestDto : IDto
    {
        public string Filter { get; set; }
    }
}

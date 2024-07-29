using Core.Entities.Abstract;
using Entities.Concrete.Enums;

namespace Entities.Concrete.Dtos.SftpDownloadDtos.SftpDownloadSearchDtos
{
    public class SftpDownloadSearchResponseDto : IDto
    {
        public int Id { get; set; }
        public int CorporateId { get; set; }
        public int UserId { get; set; }
        public string SftpFileName { get; set; }
        public StatusEnum Status { get; set; }
        public DateTime Optime { get; set; }
        public bool IsActive { get; set; }
    }
}

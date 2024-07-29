using Core.Entities.Abstract;

namespace Entities.Concrete.Dtos.HcpUploadDtos.HcpUploadSearchDtos
{
    public class HcpUploadSearchRequestDto : IDto
    {
        public int CorporateId { get; set; }
        public string? RequestNumber { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}

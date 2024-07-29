using Core.Entities.Abstract;

namespace Entities.Concrete.Dtos.CorporateDtos.CorporateSearchDtos
{
    public class CorporateSearchRequestDto : IDto
    {
        public string Filter { get; set; }
    }
}

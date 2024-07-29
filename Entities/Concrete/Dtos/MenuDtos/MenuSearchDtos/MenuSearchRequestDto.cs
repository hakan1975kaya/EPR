using Core.Entities.Abstract;

namespace Entities.Concrete.Dtos.MenuDtos.MenuSearchDtos
{
    public class MenuSearchRequestDto : IDto
    {
        public string Filter { get; set; }
    }
}

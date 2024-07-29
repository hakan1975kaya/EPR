using Core.Entities.Abstract;

namespace Entities.Concrete.Dtos.MenuDtos.MenuSearchDtos
{
    public class MenuSearchResponseDto : IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int LinkedMenuId { get; set; }
        public string Path { get; set; }
        public int MenuOrder { get; set; }
        public bool IsActive { get; set; }
    }
}

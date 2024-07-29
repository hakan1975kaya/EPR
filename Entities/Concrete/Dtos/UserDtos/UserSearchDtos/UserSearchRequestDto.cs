using Core.Entities.Abstract;

namespace Entities.Concrete.Dtos.UserDtos.UserSearchDtos
{
    public class UserSearchRequestDto : IDto
    {
        public string Filter { get; set; }
    }
}

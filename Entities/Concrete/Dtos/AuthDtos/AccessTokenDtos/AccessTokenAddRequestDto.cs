using Core.Entities.Abstract;

namespace Entities.Concrete.Dtos.AuthDtos.AccessTokenDtoS
{
    public class AccessTokenAddRequestDto:IDto//test
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
    }
}

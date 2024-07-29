using Core.Entities.Abstract;

namespace Entities.Concrete.Dtos.AuthDtos.LoginDtos
{
    public class LoginRequestDto : IDto
    {
        public int RegistrationNumber { get; set; }
        public string Password { get; set; }
    }
}

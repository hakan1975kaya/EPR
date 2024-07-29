using Core.Entities.Abstract;

namespace Entities.Concrete.Dtos.UserDtos.UserPasswordChangeDtos
{
    public class PasswordChangeRequestDto : IDto
    {
        public int RegistrationNumber { get; set; }
        public string Password { get; set; }
    }
}

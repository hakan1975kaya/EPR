﻿using Core.Entities.Abstract;

namespace Entities.Concrete.Dtos.UserDtos.UserAddDtos
{
    public class UserAddRequestDto : IDto
    {
        public int Id { get; set; }
        public int RegistrationNumber { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsActive { get; set; }
    }
}

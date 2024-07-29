﻿using Core.Entities.Abstract;

namespace Entities.Concrete.Dtos.UserRoleDtos.UserRoleUpdateDtos
{
    public class UserRoleUpdateRequestDto : IDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public DateTime Optime { get; set; }
        public bool IsActive { get; set; }
    }
}

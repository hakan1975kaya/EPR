using Core.Entities.Abstract;
using Entities.Concrete.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.Dtos.RoleDtos.RoleAddDtos
{
    public class RoleAddRequestDto : IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Optime { get; set; }
        public bool IsActive { get; set; }
    }
}

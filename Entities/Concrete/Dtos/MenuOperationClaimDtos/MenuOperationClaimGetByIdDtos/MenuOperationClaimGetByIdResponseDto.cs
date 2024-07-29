using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.Dtos.MenuOperationClaimDtos.MenuOperationClaimGetByIdDtos
{
    public class MenuOperationClaimGetByIdResponseDto : IDto
    {
        public int Id { get; set; }
        public int MenuId { get; set; }
        public int OperationClaimId { get; set; }
        public bool IsActive { get; set; }
    }
}

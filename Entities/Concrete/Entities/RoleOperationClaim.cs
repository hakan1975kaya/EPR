using Core.Entities.Abstract;
using Entities.Concrete.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Concrete.Entities
{
    public class RoleOperationClaim : IEntity
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int OperationClaimId { get; set; }
        public DateTime Optime { get; set; }
        public bool IsActive { get; set; }

    }
}



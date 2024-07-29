using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities.Abstract;

namespace Core.Entities.Concrete.Entities
{
    public class OperationClaim : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int LinkedOperationClaimId { get; set; }
        public bool IsActive { get; set; }
    }
}

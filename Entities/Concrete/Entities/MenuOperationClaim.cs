using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.Entities
{
    public class MenuOperationClaim : IEntity
    {
        public int Id { get; set; }
        public int MenuId { get; set; }
        public int OperationClaimId { get; set; }
        public bool IsActive { get; set; }
    }
}

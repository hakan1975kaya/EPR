using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.Entities
{
    public class HcpUpload : IEntity
    {
        public int Id { get; set; }
        public int PaymentRequestId { get; set; }
        public Guid HcpId { get; set; }
        public string Extension { get; set; }
        public string Explanation { get; set; }
        public DateTime Optime { get; set; }
        public bool IsActive { get; set; }
    }
}

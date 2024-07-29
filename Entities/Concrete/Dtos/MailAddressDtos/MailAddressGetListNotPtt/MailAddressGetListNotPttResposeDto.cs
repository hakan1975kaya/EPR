using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Aspects.Autofac.Performance.Dtos
{
    public class MailAddressGetListNotPttResposeDto
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public bool IsCC { get; set; }
        public bool IsPtt { get; set; }
        public DateTime Optime { get; set; }
        public bool IsActive { get; set; }
    }
}

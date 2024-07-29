using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.Enums
{
   public enum AuditEnum
    {
        None = 0,
        Debug = 1,
        Info =2,  
        Warn=3,
        Error = 4,
        Fatal = 5
    }
}

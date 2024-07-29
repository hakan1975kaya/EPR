using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.Enums
{
    public enum StatusEnum
    {
        SummaryWaitingForApproval = 0,
        SummaryApproved = 1,
        SummaryRejected = 2,
        DownloadedFromSftp=3,
        DownloadedFromApi=4
    }
}

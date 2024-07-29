using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Logging
{
    public class LogDetail
    {
        public string MethodName { get; set; }
        public List<LogParameter> LogParameters { get; set; }
        public DateTime Date { get; set; }
        public int RegistrationNumber { get; set; }
        public string RemoteIpAddress { get; set; }
        public string Response { get; set; }
    }
}

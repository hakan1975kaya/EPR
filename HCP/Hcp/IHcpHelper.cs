using HcpServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Hcp
{
    public interface IHcpHelper
    {
        Task<fileDownloadResponse> GetFileByName(string fileName);
        Task<HttpResponseMessage> AddFile(string fileName, byte[] fileByte);
        Task<HttpResponseMessage> DownloadFile(string fileName);
        Task<HttpResponseMessage> FileDelete(string fileName);
    }
}

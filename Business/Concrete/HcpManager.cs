using Business.Abstract;
using Core.Utilities.Hcp;
using HcpServiceReference;

namespace Business.Concrete
{
    public class HcpManager : IHcpService
    {
        private IHcpHelper _hcpHelper;
        public HcpManager(IHcpHelper hcpHelper)
        {
            _hcpHelper = hcpHelper;
        }
        public async Task<HttpResponseMessage> AddFile(string fileName, byte[] fileByte)
        {
            return await _hcpHelper.AddFile(fileName, fileByte);
        }
        public async Task<HttpResponseMessage> DownloadFile(string fileName)
        {
            return await _hcpHelper.DownloadFile(fileName);
        }
        public async Task<HttpResponseMessage> FileDelete(string fileName)
        {
            return await _hcpHelper.FileDelete(fileName);
        }
        public async Task<fileDownloadResponse> GetFileByName(string fileName)
        {
            return await _hcpHelper.GetFileByName(fileName);
        }
    }
}

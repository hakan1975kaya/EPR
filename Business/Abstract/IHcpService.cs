using HcpServiceReference;
namespace Business.Abstract
{
    public interface IHcpService
    {
        Task<fileDownloadResponse> GetFileByName(string fileName);
        Task<HttpResponseMessage> AddFile(string fileName, byte[] fileByte);
        Task<HttpResponseMessage> DownloadFile(string fileName);
        Task<HttpResponseMessage> FileDelete(string fileName);
    }
}

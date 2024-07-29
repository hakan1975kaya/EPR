using HcpServiceReference;
using System.Globalization;
using System.Net.Http.Headers;
using System.Security.Authentication;
using HcpServiceReference;
using System.Globalization;
using System.Net.Http.Headers;
using System.Security.Authentication;

namespace Core.Utilities.Hcp
{
    public class HcpHelper : IHcpHelper
    {
        public async Task<fileDownloadResponse> GetFileByName(string fileName)
        {
            FileOperationsPortTypeClient fileOperationsPortTypeClient = new(new FileOperationsPortTypeClient.EndpointConfiguration());
            var fileDownloadResponse = await fileOperationsPortTypeClient
            .fileDownloadAsync(
                "cHR0YmFua2Rlcm5la29kZW1lbGVyaQ==",
                "4ffe562558ddd519d9eda3b7f79f9202",
               fileName
            );
            fileOperationsPortTypeClient.Close();
            return fileDownloadResponse;
        }
        public async Task<HttpResponseMessage> AddFile(string fileName, byte[] fileByte)
        {
            FileOperationsPortTypeClient fileOperationsPortTypeClient = new(new FileOperationsPortTypeClient.EndpointConfiguration());
            var fileUploadResponse = await fileOperationsPortTypeClient
            .fileUploadAsync(
                "cHR0YmFua2Rlcm5la29kZW1lbGVyaQ==",
                "4ffe562558ddd519d9eda3b7f79f9202",
                fileByte,
               fileName
            );
            fileOperationsPortTypeClient.Close();
            return new HttpResponseMessage();
        }
        public async Task<HttpResponseMessage> DownloadFile(string fileName)
        {
            FileOperationsPortTypeClient fileOperationsPortTypeClient = new(new FileOperationsPortTypeClient.EndpointConfiguration());
            fileDownloadResponse fileDownloadResponse = await fileOperationsPortTypeClient
            .fileDownloadAsync(
                "cHR0YmFua2Rlcm5la29kZW1lbGVyaQ==",
                "4ffe562558ddd519d9eda3b7f79f9202",
               fileName
            );
            fileOperationsPortTypeClient.Close();
            return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
        }
        public async Task<HttpResponseMessage> FileDelete(string fileName)
        {
            FileOperationsPortTypeClient fileOperationsPortTypeClient = new(new FileOperationsPortTypeClient.EndpointConfiguration());
            var fileDeleteResponse = await fileOperationsPortTypeClient
            .fileDeleteAsync(
                "cHR0YmFua2Rlcm5la29kZW1lbGVyaQ==",
                "4ffe562558ddd519d9eda3b7f79f9202",
               fileName
            );
            fileOperationsPortTypeClient.Close();
            return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
        }
       
       

    }
}



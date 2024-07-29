using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sftp.Dtos.SftpDownloadFileDtos
{
    public class SftpDownloadFileRequestDto
    {
        public string SftpFileName { get; set; }
        public string Prefix { get; set; }
    }
}

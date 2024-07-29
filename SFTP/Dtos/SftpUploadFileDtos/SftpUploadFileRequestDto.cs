using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sftp.Dtos.SftpUploadFileDtos
{
    public class SftpUploadFileRequestDto
    {
        public string FilePath { get; set; }
        public Stream Stream { get; set; }
    }
}

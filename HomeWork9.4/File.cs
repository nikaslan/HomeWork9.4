using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork9._4
{
    class File
    {
        private int id {get;set; }
        private int fileId;
        private string fileType, name, uploadTime ;

        public File(int fileId, string fileName, string fileType, string uploadTime)
        {
            this.fileId = fileId;
            this.name = fileName;
            this.fileType = fileType;
            this.uploadTime = uploadTime;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VimeoDownloader
{
    /// <summary>
    /// 다운로드 진행 이벤트 매개변수
    /// </summary>
    public class ProgressEventArgs : EventArgs
    {
        public long WriteBytes { get; private set; }
        public long TotalBytes { get; private set; }
         
        public ProgressEventArgs( long writeBytes, long totalBytes): base()
        {
            WriteBytes = writeBytes;
            TotalBytes = totalBytes;
        }
    }
}

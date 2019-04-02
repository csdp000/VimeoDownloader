using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VimeoDownloader
{
    public class ProgressEventArgs : EventArgs
    {
        public long WriteBytes { get; private set; }
        public long TotalBytes { get; private set; }

        public ProgressEventArgs(object sender, long writeBytes, long totalBytes): base()
        {
            WriteBytes = writeBytes;
            TotalBytes = totalBytes;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VimeoDownloader
{
    public class VideoContextEventArgs : EventArgs
    {
        public VimeoDownloader.Models.VimeoInfo VimeoInfo{ get; private set; } 

        public VideoContextEventArgs(VimeoDownloader.Models.VimeoInfo vimeoInfo) : base()
        {
            VimeoInfo = vimeoInfo;
        }
    }
}

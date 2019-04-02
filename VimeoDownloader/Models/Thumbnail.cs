using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VimeoDownloader.Models
{
    public class Thumbnail
    {
        /// <summary>
        /// 썸네일 이미지 Url
        /// </summary>
        public string Url { get; set; }
        
        /// <summary>
        /// 썸네일 이미지 해상도 품질
        /// </summary>
        public string Resolution { get; set; } 
    }
}

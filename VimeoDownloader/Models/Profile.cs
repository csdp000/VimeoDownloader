using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VimeoDownloader.Models
{
    public class Profile
    {
        /// <summary>
        /// 동영상 다운로드 Url
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 동영상 가로 해상도
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        /// 동영상 세로 해상도
        /// </summary>
        public int Height { get; set; }
        /// <summary>
        /// 동영상 Fps
        /// </summary>
        public int Fps { get; set; }
        /// <summary>
        /// 동영상 Mime Type
        /// </summary>
        public string Mime { get; set; }
        /// <summary>
        /// 동영상 품질
        /// </summary>
        public string Quality { get; set; }
        /// <summary>
        /// 동영상 파일 크기
        /// </summary>
        public long Length { get; set; }

        public override string ToString()
        {
            return $"Url: {Url}{Environment.NewLine}Width: {Width}{Environment.NewLine}Height: {Height}{Environment.NewLine}File Length: {Length}{Environment.NewLine}Fps: {Fps}{Environment.NewLine}Mime: {Mime}{Environment.NewLine}Quality: {Quality}{Environment.NewLine}";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VimeoDownloader.Models
{
    public class Video
    {
        /// <summary>
        /// vimeo 동영상 Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 동영상 세로 해상도
        /// </summary>
        public int Height { get; set; }
        /// <summary>
        /// 동영상 가로 해상도
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        /// 동영상 재생 시간
        /// </summary>
        public int Duration { get; set; }
        /// <summary>
        /// 동영상 fps
        /// </summary>
        public float Fps { get; set; }
        /// <summary>
        /// 동영상 썸네일
        /// </summary>
        public string Thumb { get; set; }
        /// <summary>
        /// 동영상 Profile
        /// </summary>
        public Profile[] Profiles { get; set; }

        public override string ToString()
        {
            return $"ID: {Id}{Environment.NewLine}Width: {Width}{Environment.NewLine}Height: {Height}{Environment.NewLine}Duration: {Duration}{Environment.NewLine}Fps: {Fps}{Environment.NewLine}Thumb: {Thumb}";
        }
    }
}

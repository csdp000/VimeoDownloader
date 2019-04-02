﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VimeoDownloader.Enums;
using VimeoDownloader.Exceptions;

namespace VimeoDownloader.Models
{
    public class VimeoInfo
    {
        private IList<KeyValuePair<VideoQuality,string>> _qualities = new List<KeyValuePair<VideoQuality, string>>()
        {
            new KeyValuePair<VideoQuality, string>( VideoQuality.High, "2160p"),
            new KeyValuePair<VideoQuality, string>( VideoQuality.High, "1440p"),
            new KeyValuePair<VideoQuality, string>(VideoQuality.High, "1080p"),
            new KeyValuePair<VideoQuality, string>(VideoQuality.Medium, "720p"),
            new KeyValuePair<VideoQuality, string>(VideoQuality.Medium, "540p"),
            new KeyValuePair<VideoQuality, string>(VideoQuality.Medium, "480p"),
            new KeyValuePair<VideoQuality, string>(VideoQuality.Low, "360p"),
            new KeyValuePair<VideoQuality, string>(VideoQuality.Low, "240p"),
            new KeyValuePair<VideoQuality, string>(VideoQuality.Low, "144p")
        };

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
        public Profile GetProfile(VideoQuality quality)
        { 
            if (Profiles.Length < 1 )
                throw new VimeoException("Profiles Length less than 1");

            Profile profile = null;

            //내림차순 정렬
            Array.Sort(Profiles, (x, y) => y.Height.CompareTo(x.Height));

#if DEBUG
            for (int i=0;i<Profiles.Length; i++) 
                Console.WriteLine(Profiles[i].Quality);
#endif
            
            //조건에 맞는 영상 품질 리스트를 뽑아옴
            var selectQualities = from qual in _qualities
                        where qual.Key == quality
                        select qual.Value;

            //뽑아와서 조건에 부합하는 첫번쨰 요소 반환
            profile = Profiles.FirstOrDefault(c => selectQualities.Contains(c.Quality));


            // 퀄리티 리스트에서 찾지 못 할 경우
            if (profile == null) 
                profile = (quality == VideoQuality.Low) ? Profiles.Last() : Profiles.First();

            return profile;
        }
    }
}

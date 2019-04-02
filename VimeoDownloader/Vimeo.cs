using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq; 
using System.Net;
using System.IO;
using VimeoDownloader.Models;
using VimeoDownloader.Exceptions;
using VimeoDownloader.Enums; 
using System.Net.Http;

namespace VimeoDownloader
{
    public class Vimeo
    {
        public event EventHandler DownloadStarted;
        public event EventHandler DownloadCancel;
        public event EventHandler DownloadFinished;
        public event EventHandler<ProgressEventArgs> DownloadProgress; 
        public string DownloadPath { get; set; }

        /// <summary>
        /// 동영상을 다운로드 (파일 이름을 동영상 제목으로 지정)
        /// </summary>
        /// <param name="path">파일이 저장될 경로</param>
        /// <param name="vimeoInfo">VimeoInfo 객체</param> 
        /// <returns></returns>
        public async Task Download(string path, VimeoInfo vimeoInfo) { await Download(path, vimeoInfo, VideoQuality.High, vimeoInfo.Title); }
 
        /// <summary>
        /// 동영상을 다운로드
        /// </summary>
        /// <param name="path">파일이 저장될 경로</param>
        /// <param name="vimeoInfo">VimeoInfo 객체</param> 
        /// <param name="quality">동영상 품질</param>
        /// <returns></returns>
        public async Task Download(string path, VimeoInfo vimeoInfo,VideoQuality quality) { await Download(path, vimeoInfo, quality, vimeoInfo.Title); }

        /// <summary>
        /// 동영상을 다운로드
        /// </summary>
        /// <param name="path">파일이 저장될 경로</param>
        /// <param name="vimeoInfo">VimeoInfo 객체</param>
        /// <param name="quality">동영상 품질</param>
        /// <param name="fileName">확장자를 제외한 파일이름</param>
        /// <returns></returns>
        public async Task Download(string path, VimeoInfo vimeoInfo,VideoQuality quality,string fileName)
        { 
            string saveFile = $@"{path}\{fileName}{MimeType.GetExtension(vimeoInfo.GetProfile(VideoQuality.High).Mime)}";

            var profile = vimeoInfo.GetProfile(quality);
             
            HttpClient client = new HttpClient(); 
            var response = await client.GetAsync(profile.Url); 
            const int bufferSize = 1024;
            using (var stream = await response.Content.ReadAsStreamAsync())
            using (var writer = new FileStream(saveFile,FileMode.Create,FileAccess.Write))
            {

                var buffer = new byte[bufferSize];
                int bytes;
                long writeBytes = 0;
                
                while ((bytes = stream.Read(buffer, 0, bufferSize)) > 0)
                {
                    writeBytes += bytes; 
                    writer.Write(buffer, 0, bytes); 
                    DownloadProgress?.Invoke(this, new ProgressEventArgs(this, writeBytes, (long)profile.Length));

                }
            }

            Console.WriteLine(saveFile);
        }
        public Stream GetStream()
        {
            throw new NotImplementedException(); 
        }
         
        public static async Task<VimeoInfo> GetVimeoInfo(string videoId)
        {
            if (string.IsNullOrEmpty(videoId))
                throw new ArgumentNullException("videoId");

            VimeoInfo vimeoInfo = null;

            try
            {
                using (var request = new HttpClient())
                { 
                    var body = await request.GetStringAsync($"https://player.vimeo.com/video/{videoId}/config");

                    var jobj = JObject.Parse(body);

                    vimeoInfo = jobj["video"].ToObject<VimeoInfo>();
                    vimeoInfo.Thumb = jobj["video"]["thumbs"]["base"]?.ToString();
                    vimeoInfo.Profiles = jobj["request"]["files"]["progressive"].ToObject<Profile[]>();

                    vimeoInfo.Title = MimeType.MakeValidFileName(vimeoInfo.Title);
                    //내림차순 정렬
                    Array.Sort(vimeoInfo.Profiles, (x, y) => y.Height.CompareTo(x.Height));

                    //영상 각 품질별 파일크기를 구함
                    foreach (var profile in vimeoInfo.Profiles)
                    {
                        var message = new HttpRequestMessage(HttpMethod.Head, profile.Url);
                        using (var response = await request.SendAsync(message))
                        {
                            profile.Length = response.Content.Headers.ContentLength;
                        } 
                    }
                }
            }
            catch(Exception ex)
            {
                throw new VimeoParseException(videoId, ex);
            }
            return vimeoInfo;
        }
    }
}

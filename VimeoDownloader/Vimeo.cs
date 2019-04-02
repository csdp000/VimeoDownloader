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

using System.Net.Http;

namespace VimeoDownloader
{
    public class Vimeo
    {
        public EventHandler DownloadStarted;
        public EventHandler DownloadCancel;
        public EventHandler DownloadFinished;
        public EventHandler<ProgressEventArgs> DownloadProgress;

        public async void Download()
        {
            throw new NotImplementedException();
        }

        public async Task<VimeoInfo> GetVideoInfo(string videoId)
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
#if DEBUG
                    Console.WriteLine(vimeoInfo);
#endif

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

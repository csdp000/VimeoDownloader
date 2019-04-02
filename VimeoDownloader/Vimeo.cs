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

        public async Task<Video> GetVideoInfo(string videoId)
        {
            if (string.IsNullOrEmpty(videoId))
                throw new ArgumentNullException("videoId");

            Video vimeo = null;

            try
            {
                using (var request = new HttpClient())
                { 
                    var body = await request.GetStringAsync($"https://player.vimeo.com/video/{videoId}/config");

                    var jobj = JObject.Parse(body);

                    vimeo = jobj["video"].ToObject<Video>();
                    vimeo.Thumb = jobj["video"]["thumbs"]["base"]?.ToString();
                    vimeo.Profiles = jobj["request"]["files"]["progressive"].ToObject<Profile[]>();
                }
            }
            catch(Exception ex)
            {
                throw new VimeoParseException(videoId, ex);
            }
            return vimeo;
        }
    }
}

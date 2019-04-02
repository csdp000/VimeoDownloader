﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq; 
using System.Net;
using System.IO;
using System.Drawing;
using System.Net.Http;

using VimeoDownloader.Models;
using VimeoDownloader.Exceptions;
using VimeoDownloader.Enums;  
namespace VimeoDownloader
{
    public class Vimeo
    {
        /// <summary>
        /// 동영상 다운로드 시작 이벤트
        /// </summary>
        public event EventHandler DownloadStarted;
        
        /// <summary>
        /// 동영상 다운로드 취소 이벤트
        /// </summary>
        public event EventHandler DownloadCancel;
        
        /// <summary>
        /// 동영상 다운로드 완료 이벤트
        /// </summary>
        public event EventHandler DownloadFinished;
        
        /// <summary>
        /// 동영상 다운로드 진행 이벤트
        /// </summary>
        public event EventHandler<ProgressEventArgs> DownloadProgress; 
          
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
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException("path");
            if (vimeoInfo == null)
                throw new ArgumentNullException("vimeoInfo");
            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentNullException("fileName");

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
                    DownloadProgress?.Invoke(this, new ProgressEventArgs(this, writeBytes, profile.Length)); 
                }
            } 
        }

        /// <summary>
        /// 동영상 다운로드 읽기스트림을 반환합니다.
        /// </summary>
        /// <param name="vimeoInfo">VimeoInfo 객체</param>
        /// <returns>읽기 스트림 반환</returns>
        public async Task<Stream> GetStream(VimeoInfo vimeoInfo) { return await GetStream(vimeoInfo, VideoQuality.High); }

        /// <summary>
        /// 동영상 다운로드 읽기스트림을 반환합니다.
        /// </summary>
        /// <param name="vimeoInfo">VimeoInfo 객체</param>
        /// <param name="quality">동영상 품질</param>
        /// <returns>읽기 스트림 반환</returns>
        public async Task<Stream> GetStream(VimeoInfo vimeoInfo, VideoQuality quality)
        {
            if (vimeoInfo == null)
                throw new ArgumentNullException("vimeoInfo");

            var profile = vimeoInfo.GetProfile(quality);

            HttpClient client = new HttpClient(); 
            var response = await client.GetAsync(profile.Url);

            return await response.Content.ReadAsStreamAsync();
        }

        /// <summary>
        /// 동영상 썸네일 이미지를 Image 객체로 반환합니다.
        /// </summary>
        /// <param name="vimeoInfo">VimeoInfo 객체</param> 
        /// <returns>Image 객체 반환</returns> 
        public async Task<Image> GetThumbnail(VimeoInfo vimeoInfo) { return await GetThumbnail(vimeoInfo, ThumbnailQuality.Low); }

        /// <summary>
        /// 동영상 썸네일 이미지를 Image 객체로 반환합니다.
        /// </summary>
        /// <param name="vimeoInfo">VimeoInfo 객체</param>
        /// <param name="quality">썸네일 품질</param>
        /// <returns>Image 객체 반환</returns> 
        public async Task<Image> GetThumbnail(VimeoInfo vimeoInfo,ThumbnailQuality quality)
        { 
            if (vimeoInfo == null)
                throw new ArgumentNullException("vimeoInfo");

            Image image = null;

            try
            {
                var thumb =  vimeoInfo.GetThumbnail(quality);

                HttpClient client = new HttpClient();
                var response = await client.GetAsync(thumb.Url);

                image = Image.FromStream(await response.Content.ReadAsStreamAsync());
            }
            catch(Exception ex)
            {
                throw new VimeoException("Thumbnail Image Download Error", ex);
            } 
            return image;
        }

        /// <summary>
        /// 동영상 정보를 구합니다.
        /// </summary>
        /// <param name="videoId">비메오 동영상 Id</param>
        /// <returns>VimeoInfo 객체 반환</returns>
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
                    vimeoInfo.Profiles = jobj["request"]["files"]["progressive"].ToObject<Profile[]>();
                     

                    var thumbnailList = new List<KeyValuePair<string, string>>();
                    foreach(var kvp in jobj["video"]["thumbs"].ToObject<JObject>()) 
                        thumbnailList.Add(new KeyValuePair<string, string>(kvp.Key,kvp.Value.ToString()));

                    //썸네일 리스트
                    vimeoInfo.Thumbnail = thumbnailList.Select((x) => new Thumbnail { Url = x.Value, Resolution = x.Key }).ToArray();
                     
                    vimeoInfo.Title = MimeType.MakeValidFileName(vimeoInfo.Title);

                    //내림차순 정렬
                    Array.Sort(vimeoInfo.Profiles, (x, y) => y.Height.CompareTo(x.Height));

                    //영상 각 품질별 파일크기를 구함
                    foreach (var profile in vimeoInfo.Profiles)
                    {
                        var message = new HttpRequestMessage(HttpMethod.Head, profile.Url);
                        using (var response = await request.SendAsync(message))
                        {
                            profile.Length = response.Content.Headers.ContentLength ?? -1; 
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

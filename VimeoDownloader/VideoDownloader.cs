using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VimeoDownloader
{
    public abstract class VideoDownloader
    {
        /// <summary>
        /// 동영상 다운로드 시작 이벤트
        /// </summary>
        public event EventHandler<VideoContextEventArgs> DownloadStarted;

        /// <summary>
        /// 동영상 다운로드 취소 이벤트
        /// </summary>
        public event EventHandler<VideoContextEventArgs> DownloadCancel;

        /// <summary>
        /// 동영상 다운로드 완료 이벤트
        /// </summary>
        public event EventHandler<VideoContextEventArgs> DownloadFinished;

        /// <summary>
        /// 동영상 다운로드 진행 이벤트
        /// </summary>
        public event EventHandler<ProgressEventArgs> DownloadProgress;
          
        protected void OnDownloadStarted(VideoContextEventArgs arg) { DownloadStarted?.Invoke(this, arg); }
        protected void OnDownloadCancel(VideoContextEventArgs arg) { DownloadCancel?.Invoke(this, arg); }
        protected void OnDownloadFinished(VideoContextEventArgs arg) { DownloadFinished?.Invoke(this, arg); }
        protected void OnDownloadProgress(ProgressEventArgs arg) { DownloadProgress?.Invoke(this, arg); }
    }
}

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
          
        protected void OnDownloadStarted(EventArgs arg) { DownloadStarted?.Invoke(this, arg); }
        protected void OnDownloadCancel(EventArgs arg) { DownloadCancel?.Invoke(this, arg); }
        protected void OnDownloadFinished(EventArgs arg) { DownloadFinished?.Invoke(this, arg); }
        protected void OnDownloadProgress(ProgressEventArgs arg) { DownloadProgress?.Invoke(this, arg); }
    }
}

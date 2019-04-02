using System;


namespace VimeoDownloader.Exceptions
{
    /// <summary>
    /// Vimeo 파싱 에러 Exception
    /// </summary>
    public class VimeoParseException : Exception
    {
        public VimeoParseException(string videoId, Exception innerException)
            : base($"{videoId}", innerException)
        {
        }
    }
}

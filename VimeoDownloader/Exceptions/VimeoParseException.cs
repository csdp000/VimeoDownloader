using System;


namespace VimeoDownloader.Exceptions
{
    /// <summary>
    /// Vimeo 파싱 에러 Exception
    /// </summary>
    public class VimeoParseException : Exception
    {
        public VimeoParseException(string vimeoId, Exception innerException)
            : base($"{vimeoId} 동영상을 파싱하는데 문제가 생겼습니다.", innerException)
        {
        }
    }
}

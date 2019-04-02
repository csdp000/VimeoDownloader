using System;


namespace VimeoDownloader.Exceptions
{
    public class VimeoParseException : Exception
    {
        public VimeoParseException(string videoId, Exception innerException)
            : base($"{videoId}", innerException)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VimeoDownloader.Exceptions
{
    public class VimeoException : Exception
    {
        public VimeoException(string message, Exception innerException)
            : base($"{message}", innerException)
        {
        }
        public VimeoException(string message)
            : base($"{message}")
        {
        }
    }
}

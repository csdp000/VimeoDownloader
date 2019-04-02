using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace VimeoDownloader
{
    public class UrlHelper
    {
        /// <summary>
        /// 문자열에서 URL을 뽑아옵니다.
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string[] GetUrlFromString(string content)
        {
            const string pattern = @"(http|https):\/\/([\w\-_]+(?:(?:\.[\w\-_]+)+))([\w\-\.,@?^=%&amp;:\/~\+#]*[\w\-\@?^=%&amp;\/~\+#])?"; 
            var regex = new Regex(pattern);
            return regex.Matches(content).Cast<Match>().Select(c => c.Value).ToArray();
        } 
    }
}

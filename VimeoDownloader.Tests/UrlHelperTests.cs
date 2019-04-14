using Microsoft.VisualStudio.TestTools.UnitTesting;
using VimeoDownloader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VimeoDownloader.Tests
{
    [TestClass()]
    public class UrlHelperTests
    {
        [TestMethod()]
        public void GetUrlFromString_ReturnUrls()
        { 
            string content =
@"TestUrl: http://naver.com
TestUrl2: http://daum.net
TestUrl3 http://github.com/ TestUrl3
TestUrl4 https://github.com/csdp000/VimeoDownloader TestUrl4";

            var urls = UrlHelper.GetUrlsFromString(content);

            Assert.AreEqual("http://naver.com", urls[0]);
            Assert.AreEqual("http://daum.net", urls[1]);
            Assert.AreEqual("http://github.com/", urls[2]);
            Assert.AreEqual("https://github.com/csdp000/VimeoDownloader", urls[3]);
        }
    }
}
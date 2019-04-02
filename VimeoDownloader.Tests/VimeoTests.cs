using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VimeoDownloader;
using VimeoDownloader.Models;

namespace VimeoDownloader.Tests
{
    [TestClass()]
    public class VimeoTests
    {
        [TestMethod()]
        public  void GetVideoInfo_ReturnVideoMetaData()
        {
            var downloader = new Vimeo();
            var vimeo =  downloader.GetVideoInfo("308518479").Result;

            Assert.AreNotEqual(0, vimeo.Id);
            Assert.AreNotEqual(0, vimeo.Thumb);
            Assert.AreNotEqual(0, vimeo.Profiles.Length);
        }
    }
}

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VimeoDownloader;
using VimeoDownloader.Models;

namespace VimeoDownloader.Tests
{
    [TestClass()]
    public class VimeoTests
    {
        private const string TestVideoId = "308518479";

        [TestMethod()]
        public  void GetVideoInfo_ReturnVideoMetaData()
        {
            var downloader = new Vimeo();
            var vimeo =  downloader.GetVideoInfo(TestVideoId).Result;

            Assert.AreNotEqual(0, vimeo.Id);
            Assert.AreNotEqual(0, vimeo.Thumb);
            Assert.AreNotEqual(0, vimeo.Profiles.Length);
        }

        [TestMethod()]
        public void GetProfile_Return1080pProfile()
        {
            var downloader = new Vimeo();
            var vimeo = downloader.GetVideoInfo(TestVideoId).Result;
            var profile = vimeo.GetProfile(Enums.VideoQuality.High);
            Assert.AreEqual("1080p", profile.Quality);
        }
        [TestMethod()]
        public void GetProfile_Return720pProfile()
        {
            var downloader = new Vimeo();
            var vimeo = downloader.GetVideoInfo(TestVideoId).Result;
            var profile = vimeo.GetProfile(Enums.VideoQuality.Medium);
            Assert.AreEqual("720p", profile.Quality);
        }
        [TestMethod()]
        public void GetProfile_Return360pProfile()
        {
            var downloader = new Vimeo();
            var vimeo = downloader.GetVideoInfo(TestVideoId).Result;
            var profile = vimeo.GetProfile(Enums.VideoQuality.Low);
            Assert.AreEqual("360p", profile.Quality);
        }
    }
}

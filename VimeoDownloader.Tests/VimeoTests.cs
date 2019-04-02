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
        private VimeoInfo videoInfo = null;

        [TestInitialize]
        public void Initalize()
        {
            videoInfo = Vimeo.GetVimeoInfo(TestVideoId).Result;
        }
        [TestMethod()]
        public  void GetVideoInfo_ReturnVideoMetaData()
        { 
            Assert.AreNotEqual(0, videoInfo.Id);
            Assert.AreNotEqual(0, videoInfo.Thumbnail.Length);
            Assert.AreNotEqual(0, videoInfo.Profiles.Length);
        } 
        [TestMethod()]
        public void GetProfile_Return1080pProfile()
        {
            var downloader = new Vimeo(); 
            var profile = videoInfo.GetProfile(Enums.VideoQuality.High);
            Assert.AreEqual("1080p", profile.Quality); 
        }
        [TestMethod()]
        public void GetProfile_Return720pProfile()
        {
            var downloader = new Vimeo(); 
            var profile = videoInfo.GetProfile(Enums.VideoQuality.Medium);
            Assert.AreEqual("720p", profile.Quality);
        }
        [TestMethod()]
        public void GetProfile_Return360pProfile()
        {
            var downloader = new Vimeo(); 
            var profile = videoInfo.GetProfile(Enums.VideoQuality.Low);
            Assert.AreEqual("360p", profile.Quality);
        }
    }
}

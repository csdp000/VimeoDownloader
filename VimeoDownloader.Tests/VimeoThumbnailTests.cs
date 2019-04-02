using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VimeoDownloader;
using VimeoDownloader.Models;

namespace VimeoDownloader.Tests
{
    [TestClass()]
    public class VimeoThumbnailTests
    {
        private const string TestVideoId = "308518479";
        private VimeoInfo videoInfo = null;

        [TestInitialize]
        public void Initalize()
        {
            videoInfo = Vimeo.GetVimeoInfo(TestVideoId).Result;
        }
        /*
        [TestMethod()]
        public void ThumbnailCount_ReturnMoreThan0()
        { 
        } 
        */
    }
}

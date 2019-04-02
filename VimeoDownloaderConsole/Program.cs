using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VimeoDownloader;
using VimeoDownloader.Enums;
using VimeoDownloader.Models;
using System.IO;

namespace VimeoDownloaderConsole
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string videoId = "308518479";

            Vimeo vimeo = new Vimeo();
            var vimeoInfo = await Vimeo.GetVimeoInfo(videoId); 
            foreach (var vInfo in vimeoInfo.Profiles)
            {
                Console.WriteLine(vInfo);
            }
            var profile = vimeoInfo.GetProfile(VideoQuality.High);

            Console.WriteLine($"High Quality: {Environment.NewLine}{profile}");
            vimeo.DownloadProgress += (sedner, arg) =>
            { 
                Console.WriteLine($"{arg.WriteBytes} / {arg.TotalBytes}  ({ (arg.WriteBytes*1.0/arg.TotalBytes)*100 })%)");
            };
            await vimeo.Download(Directory.GetCurrentDirectory(), vimeoInfo,VideoQuality.High); 
            Console.ReadKey();
        }
         
    }
}

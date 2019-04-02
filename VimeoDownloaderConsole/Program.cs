using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VimeoDownloader;
using VimeoDownloader.Enums;
using VimeoDownloader.Models;
using VimeoDownloader.Exceptions;
using System.IO; 

namespace VimeoDownloaderConsole
{

    using static System.Console;
    class Program
    { 
        static async Task Main(string[] args)
        {
            Vimeo vimeo = new Vimeo();

            string dir = Directory.GetCurrentDirectory();

            while (true)
            {
                WriteLine("Input Video ID : ");
                string text = ReadLine();
                VimeoInfo vimeoInfo = null;
                try
                {
                    vimeoInfo = await Vimeo.GetVimeoInfo(text);
                    WriteLine(vimeoInfo);
                }
                catch (VimeoParseException ex) { WriteLine($"parsing err: {ex.Message}"); }
                catch (Exception ex) { WriteLine($"err: {ex.Message}"); }

                if (vimeoInfo!=null)
                {
                    string fileName = "";

                    WriteLine("Thumbnail download? Y/N : ");
                    text = ReadLine();
                    bool thumbnailDownload = (text == "Y") ? true : false;


                    WriteLine("Type video filename (enter -> default): ");
                    text = ReadLine();
                    fileName = text;

                    WriteLine("Select video quality [ H / M / L ] : ");
                    text = ReadLine();
                    VideoQuality videoQuality = (text == "H") ? VideoQuality.High : (text == "M") ? VideoQuality.Medium : VideoQuality.Low;

                    WriteLine("Download Start? Y/N :");
                    text = ReadLine(); 
                    if (text == "Y")
                    {
                        (await vimeo.GetThumbnail(vimeoInfo, ThumbnailQuality.High)).Save($@"{dir}\{vimeoInfo.Title}.jpg");

                        await vimeo.Download(dir, vimeoInfo, videoQuality, (fileName.Length>0)?fileName:vimeoInfo.Title);
                        WriteLine("Download finished");
                    }

                }
            }  
        } 
    }
}

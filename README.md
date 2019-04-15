# VimeoDownloader

- multiple download
- usage



### multiple download

```csharp
var urlString = 
    @"
    https://player.vimeo.com/video/244606067
    https://player.vimeo.com/video/244779799
    ";
    
var urls = UrlHelper.GetUrlsFromString(urlString); // Url 목록

var savePath = "savePath";

Console.WriteLine(string.Join(Environment.NewLine, urls));

Vimeo vimeo = new Vimeo();
vimeo.DownloadStarted += (sender,  arg) =>
{
    var vInfo = arg.VimeoInfo;
    WriteLine($"{vInfo.Title} Download Started");
};
vimeo.DownloadFinished += (sender,  arg) =>
{
    var vInfo = arg.VimeoInfo;
    WriteLine($"{vInfo.Title} Download Finished");
}; 
foreach (string url in urls)
{ 
    try
    {
        var vimeoInfo = await Vimeo.GetVimeoInfo(url.Substring(url.LastIndexOf("/") + 1));

        await vimeo.Download(savePath, vimeoInfo, VideoQuality.High);
    } 
    catch (Exception ex)
    {
        WriteLine($"Error : {ex}");
    }
}
```



### usage

```csharp
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
```
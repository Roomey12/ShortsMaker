using System.Diagnostics;
using ConsoleApp1;

Console.WriteLine("Start work");

var videoMaker = new FunnyVideoMaker();
var random = new Random();
var stopwatch = new Stopwatch();
stopwatch.Start();

await SplitFamilyGuyVideosAsync();

stopwatch.Stop();
Console.WriteLine(stopwatch.ElapsedMilliseconds);

async Task MergeFunnyAndGameVideos(string funnyVideosPath)
{
    var funnyFolder = @"C:\Users\OleksandrLutsenko\Desktop\familyguy_splitted\";
    var funnyVideosCount = Directory.EnumerateFiles(funnyFolder).Count();
    var gameVideo = GetRandomVideo();
    for (var i = 1; i <= funnyVideosCount; i++)
    {
        //await videoMaker.MergeVideosAsync()

    }
}

async Task SplitFamilyGuyVideosAsync(string familyGuyDirectory = @"C:\Users\OleksandrLutsenko\Desktop\family_guy_s2",
                                     string outputDirectory = @"C:\Users\OleksandrLutsenko\Desktop\familyguy_splitted")
{
    var fgVideosCount = Directory.EnumerateFiles(familyGuyDirectory).Count();
    for (var i = 1; i <= fgVideosCount; i++)
    {
        await videoMaker.SplitFamilyGuyVideoAsync(familyGuyDirectory, $"s2e{i}",$"s_2_e_{i}", outputDirectory);
    }
    
}

string GetRandomVideo(int videosCount = 83) => $@"C:\Users\OleksandrLutsenko\Desktop\games_splitted\get_{random.Next(1, videosCount)}.mp4";

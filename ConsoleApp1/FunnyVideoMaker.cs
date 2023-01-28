using FFmpeg.NET;

namespace ConsoleApp1;

public class FunnyVideoMaker
{
    private static int _gameVideoNumber = 1;
    private const string Fonts = @"C\\:/Windows/fonts/consola.ttf";
    private readonly Engine _ffmpeg;
    public FunnyVideoMaker()
    {
        _ffmpeg = new Engine();
    }

    private async Task<double> GetVideoDurationAsync(string videoPath)
    {
        var inputFile = new InputFile(videoPath);
        var metaData = await _ffmpeg.GetMetaDataAsync(inputFile, CancellationToken.None);
        return metaData.Duration.TotalSeconds;
    }
    
    public async Task SplitFamilyGuyVideoAsync(string videoDirectory, string videoName, string splittedVideoName, string outputDirectory)
    {
        var input = @$"{videoDirectory}\{videoName}.mp4";
        var duration = await GetVideoDurationAsync(input);
        var step = 59;
        var startTime = 32;
        var endTime = startTime + step;
        var part = 1;
        while (endTime < duration)
        {
            var command1 = @$"-i {input} -ss {startTime} -to {endTime} -vf scale=640:-1,pad=ceil(iw/2)*2:ceil(ih/2)*2 {outputDirectory}\{splittedVideoName}_part_{part}.mp4";
            await _ffmpeg.ExecuteAsync(command1, CancellationToken.None);
            Console.WriteLine($"Created {part} video");
            startTime += step;
            endTime += step;
            part++;
        }
    }

    public async Task SplitGameVideoAsync(string input)
    {
        var output = @"C:\Users\OleksandrLutsenko\Desktop\games_splitted\game_";
        var duration = await GetVideoDurationAsync(input);
        var step = 59;
        var startTime = 0;
        var endTime = startTime + step;
        while (endTime < duration)
        {
            var command = $@" -i {input} -ss {startTime} -to {endTime} -filter:v ""crop=560:720:360:0,scale=640:-1,pad=ceil(iw/2)*2:ceil(ih/2)*2"" {output}{_gameVideoNumber}.mp4";
            await _ffmpeg.ExecuteAsync(command, CancellationToken.None);
            Console.WriteLine($"Created {_gameVideoNumber} video");
            startTime += step;
            endTime += step;
            _gameVideoNumber++;
        }
    }

    public async Task MergeVideosAsync(string funnyVideoPath, string gameVideoPath, string outputVideoPath, int part)
    {
        var filter = $"vstack,drawtext=fontfile={Fonts}:fontsize=72:fontcolor='yellow':text='PART {part}':bordercolor=black:borderw=5:x=(w-text_w)/2:y=(h-text_h)/2-135";
        var command = $@"-i {funnyVideoPath} -i {gameVideoPath} -filter_complex ""{filter}"" {outputVideoPath}";
        await _ffmpeg.ExecuteAsync(command, CancellationToken.None);
    }
}
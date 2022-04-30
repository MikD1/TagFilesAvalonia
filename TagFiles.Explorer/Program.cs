using System;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.ReactiveUI;
using Xabe.FFmpeg;
using Xabe.FFmpeg.Downloader;

namespace TagFiles.Explorer
{
    public class Program
    {
        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // yet and stuff might break.
        [STAThread]
        public static void Main(string[] args)
        {
            BuildAvaloniaApp()
                .StartWithClassicDesktopLifetime(args);
        }

        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .LogToTrace()
                .UseReactiveUI();

        private static async Task DownloadFfmpeg()
        {
            Progress<ProgressInfo> progress = new();
            progress.ProgressChanged += (_, info) => Console.WriteLine($"{info.DownloadedBytes} / {info.TotalBytes}");
            await FFmpegDownloader.GetLatestVersion(FFmpegVersion.Official, progress);
        }
    }
}
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xabe.FFmpeg;

namespace TagFiles.Explorer.Models;

public class FilePreviewGenerator
{
    public async Task<byte[]?> GeneratePreview(string path)
    {
        string? previewFilename = null;
        try
        {
            IMediaInfo info = await FFmpeg.GetMediaInfo(path);
            IVideoStream? videoStream = info.VideoStreams.First()?.SetCodec(VideoCodec.png);
            if (videoStream is null)
            {
                return null;
            }

            videoStream = SetPreviewSize(videoStream);

            previewFilename = Path.Combine(Directory.GetCurrentDirectory(), Guid.NewGuid() + ".png");
            await FFmpeg.Conversions.New()
                .AddStream(videoStream)
                .ExtractNthFrame(0, _ => previewFilename)
                .Start();

            byte[] preview = await File.ReadAllBytesAsync(previewFilename);
            return preview;
        }
        catch (Exception)
        {
            return null;
        }
        finally
        {
            if (!string.IsNullOrEmpty(previewFilename) && File.Exists(previewFilename))
            {
                File.Delete(previewFilename);
            }
        }
    }

    private IVideoStream SetPreviewSize(IVideoStream stream)
    {
        double ratio;
        string[] parts = stream.Ratio.Split(':');
        if (parts.Length != 2)
        {
            ratio = 1.33;
        }
        else
        {
            double width = double.Parse(parts[0]);
            double height = double.Parse(parts[1]);
            ratio = width / height;
        }

        double previewWidth;
        double previewHeight;
        if (ratio > 1)
        {
            previewWidth = MaxSize;
            previewHeight = MaxSize / ratio;
        }
        else
        {
            previewHeight = MaxSize;
            previewWidth = MaxSize * ratio;
        }

        return stream.SetSize((int)previewWidth, (int)previewHeight);
    }

    private const double MaxSize = 100;
}
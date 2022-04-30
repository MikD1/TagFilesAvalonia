using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace TagFiles.Explorer.Models;

public class FilePreviewGenerator
{
    public async Task<byte[]?> GeneratePreview(string path)
    {
        string? outputFilename = null;
        try
        {
            outputFilename = Path.Combine(Directory.GetCurrentDirectory(), Guid.NewGuid() + ".jpeg");
            string arguments = $@"-i ""{path}"" -vf scale={MaxSize}:-1 -vframes 1 ""{outputFilename}""";
            Process process = Process.Start("ffmpeg", arguments);
            await process.WaitForExitAsync();
            if (process.ExitCode != 0)
            {
                return null;
            }

            byte[] preview = await File.ReadAllBytesAsync(outputFilename);
            return preview;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return null;
        }
        finally
        {
            if (!string.IsNullOrEmpty(outputFilename) && File.Exists(outputFilename))
            {
                File.Delete(outputFilename);
            }
        }
    }

    private const double MaxSize = 100;
}
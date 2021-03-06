// using System;
// using System.Collections.Generic;
// using System.IO;
// using System.Linq;
// using System.Threading.Tasks;
// using Xabe.FFmpeg.Downloader;
//
// namespace TagFiles.Explorer.Models;
//
// public class FilesManager
// {
//     public async Task LoadFiles(string location, Action<FileInfo> action)
//     {
//         await FFmpegDownloader.GetLatestVersion(FFmpegVersion.Official);
//
//         string[] filePaths = Directory.GetFiles(location);
//         foreach (string filePath in filePaths)
//         {
//             string filename = Path.GetFileName(filePath);
//             await using Stream stream = System.IO.File.Open(filePath, System.IO.FileMode.Open);
//             FileFormat format = await GetFileFormat(stream);
//
//             // Bitmap? preview = null;
//             // if (IsImage(format))
//             // {
//             //     FileMetadata? metadata = _dbContext.FilesMetadata.SingleOrDefault(x => x.Path == file);
//             //     if (metadata is null)
//             //     {
//             //         preview = Bitmap.DecodeToWidth(stream, 200);
//             //         using MemoryStream previewStream = new MemoryStream();
//             //         preview.Save(previewStream);
//             //
//             //         metadata = new FileMetadata(file, previewStream.ToArray());
//             //         _dbContext.FilesMetadata.Add(metadata);
//             //         needSaveChanges = true;
//             //     }
//             //     else
//             //     {
//             //         using MemoryStream previewStream = new MemoryStream();
//             //         previewStream.Write(metadata.Preview);
//             //         previewStream.Seek(0, SeekOrigin.Begin);
//             //         preview = new Bitmap(previewStream);
//             //     }
//             // }
//             byte[]? preview = await MakePreview(filePath);
//             action(new FileInfo(filePath, filename, format, preview));
//         }
//     }
//
//     private async Task<FileFormat> GetFileFormat(Stream stream)
//     {
//         stream.Seek(0, SeekOrigin.Begin);
//         byte[] header = new byte[8];
//         int headerLength = await stream.ReadAsync(header, 0, header.Length);
//
//         FileFormat format = FileFormat.Unknown;
//         foreach ((FileFormat key, byte[] value) in _fileHeaders)
//         {
//             if (headerLength >= value.Length)
//             {
//                 if (header.Take(value.Length).SequenceEqual(value))
//                 {
//                     format = key;
//                     break;
//                 }
//             }
//         }
//
//         stream.Seek(0, SeekOrigin.Begin);
//         return format;
//     }
//
//     private async Task<byte[]?> MakePreview(string filePath)
//     {
//         FilePreviewGenerator generator = new FilePreviewGenerator();
//         return await generator.GeneratePreview(filePath);
//     }
//
//     /*private bool IsImage(FileFormat format)
//     {
//         return format == FileFormat.Bmp ||
//                format == FileFormat.Gif ||
//                format == FileFormat.Jpeg ||
//                format == FileFormat.Png;
//     }*/
//
//     private readonly Dictionary<FileFormat, byte[]> _fileHeaders = new()
//     {
//         { FileFormat.Jpeg, new byte[] { 0xFF, 0xD8 } },
//         { FileFormat.Bmp, new byte[] { 0x42, 0x4D } },
//         { FileFormat.Gif, new byte[] { 0x47, 0x49, 0x46 } },
//         { FileFormat.Png, new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A } },
//         { FileFormat.Pdf, new byte[] { 0x25, 0x50, 0x44, 0x46 } }
//     };
// }
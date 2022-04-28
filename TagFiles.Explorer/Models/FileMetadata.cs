using System.Collections.Generic;

namespace TagFiles.Explorer.Models;

public class FileMetadata
{
    public FileMetadata(string path, List<string> tags)
    {
        Path = path;
        Tags = tags;
    }

    public string Path { get; }

    public List<string> Tags { get; }

    // ReSharper disable once UnusedMember.Local
    private FileMetadata()
    {
        Path = default!;
        Tags = default!;
    }
}
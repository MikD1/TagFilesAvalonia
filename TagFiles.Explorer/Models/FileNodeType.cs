namespace TagFiles.Explorer.Models;

public record FileNodeType
{
    public static FileNodeType File => new(true, false, false);

    public static FileNodeType Directory => new(false, true, false);

    public static FileNodeType UpLink => new(false, false, true);

    public bool IsFile { get; }

    public bool IsDirectory { get; }

    public bool IsUpLink { get; }

    private FileNodeType(bool isFile, bool isDirectory, bool isUpLink)
    {
        IsFile = isFile;
        IsDirectory = isDirectory;
        IsUpLink = isUpLink;
    }
}
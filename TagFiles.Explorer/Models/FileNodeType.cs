namespace TagFiles.Explorer.Models;

public record FileNodeType
{
    public static FileNodeType File => new(true, false);

    public static FileNodeType Directory => new(false, true);

    public bool IsFile { get; }

    public bool IsDirectory { get; }

    private FileNodeType(bool isFile, bool isDirectory)
    {
        IsFile = isFile;
        IsDirectory = isDirectory;
    }
}
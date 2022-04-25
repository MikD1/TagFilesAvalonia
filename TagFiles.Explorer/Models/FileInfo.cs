namespace TagFiles.Explorer.Models;

public record FileInfo(
    string Path,
    string Name,
    FileFormat Format,
    byte[]? Preview);
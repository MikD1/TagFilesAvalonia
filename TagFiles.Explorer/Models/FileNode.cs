namespace TagFiles.Explorer.Models;

public record FileNode(
    string Path,
    string Name,
    bool IsDirectory);
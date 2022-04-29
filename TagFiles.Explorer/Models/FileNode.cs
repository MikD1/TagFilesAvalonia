namespace TagFiles.Explorer.Models;

public record FileNode(
    string Path,
    string Name,
    FileNodeType Type);
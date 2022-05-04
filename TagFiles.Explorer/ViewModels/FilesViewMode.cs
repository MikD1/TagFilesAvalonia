namespace TagFiles.Explorer.ViewModels;

public class FilesViewMode
{
    public static FilesViewMode CreateList() => new(true, false);

    public static FilesViewMode CreateIcons() => new(false, true);

    public bool IsList { get; }

    public bool IsIcons { get; }

    private FilesViewMode(bool isList, bool isIcons)
    {
        IsList = isList;
        IsIcons = isIcons;
    }
}
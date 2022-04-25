using System.Collections.Generic;
using Avalonia.Media.Imaging;
using TagFiles.Explorer.Models;

namespace TagFiles.Explorer.ViewModels;

public class FileViewModel : ViewModelBase
{
    public FileViewModel(string name, FileFormat format, List<string> tags, Bitmap? preview)
    {
        Name = name;
        Format = format;
        Tags = tags;
        Preview = preview;
    }

    public string Name { get; }

    public FileFormat Format { get; }

    public List<string> Tags { get; }

    public Bitmap? Preview { get; }
}
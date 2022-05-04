using System.IO;
using Avalonia.Media.Imaging;
using ReactiveUI;
using TagFiles.Explorer.Models;

namespace TagFiles.Explorer.ViewModels;

public class FileNodeViewModel : ViewModelBase
{
    public FileNodeViewModel(FileNode model)
    {
        Path = model.Path;
        Name = model.Name;
        Type = model.Type;

        if (Type.IsFile)
        {
            // TODO: move to converter?
            // LoadPreview();
        }
    }

    public string Path { get; }

    public string Name { get; }

    public FileNodeType Type { get; }

    public Bitmap? Preview
    {
        get => _preview;
        set => this.RaiseAndSetIfChanged(ref _preview, value);
    }

    private async void LoadPreview()
    {
        // TODO: Cancel if not required (IDisposable?)
        FilePreviewGenerator previewGenerator = new();
        byte[]? preview = await previewGenerator.GeneratePreview(Path);
        if (preview is not null)
        {
            Preview = new Bitmap(new MemoryStream(preview));
        }
    }

    private Bitmap? _preview;
}
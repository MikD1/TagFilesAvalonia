using Avalonia.Controls;
using ReactiveUI;
using TagFiles.Explorer.Views;

namespace TagFiles.Explorer.ViewModels;

public class OpenFolderViewModel : ViewModelBase
{
    public string? Folder
    {
        get => _folder;
        private set => this.RaiseAndSetIfChanged(ref _folder, value);
    }

    public async void OpenFolder()
    {
        OpenFolderDialog dialog = new();
        Folder = await dialog.ShowAsync(MainWindow.Instance!);
    }

    private string? _folder;
}
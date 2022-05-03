using System.IO;
using System.Linq;
using ReactiveUI;

namespace TagFiles.Explorer.ViewModels;

public class FilesViewModel : ViewModelBase
{
    public FilesViewModel(string location)
    {
        _location = default!;
        _locationParts = default!;
        _content = default!;

        SetLocation(location);
        SelectListMode();
    }

    public ViewModelBase Content
    {
        get => _content;
        private set => this.RaiseAndSetIfChanged(ref _content, value);
    }

    public string[] LocationParts
    {
        get => _locationParts;
        private set => this.RaiseAndSetIfChanged(ref _locationParts, value);
    }

    public bool CanNavigateUp
    {
        get => _canNavigateUp;
        private set => this.RaiseAndSetIfChanged(ref _canNavigateUp, value);
    }

    public void SelectListMode()
    {
        Content = new FilesListViewModel(_location, SetLocation);
    }

    public void SelectIconsMode()
    {
        Content = new FilesIconsViewModel();
    }

    public void NavigateUp()
    {
        string? parent = GetParentLocation(_location);
        if (!string.IsNullOrEmpty(parent))
        {
            SetLocation(parent);
        }
    }

    private void SetLocation(string location)
    {
        _location = location;
        LocationParts = location.Split(Path.DirectorySeparatorChar).Where(x => !string.IsNullOrEmpty(x)).ToArray();

        string? parent = GetParentLocation(location);
        CanNavigateUp = parent != null;
        SelectListMode(); // TODO: Select current mode
    }

    private string? GetParentLocation(string location)
    {
        string? parent = Directory.GetParent(location)?.FullName;
        return parent;
    }

    private string _location;
    private string[] _locationParts;
    private bool _canNavigateUp;
    private ViewModelBase _content;

    // ScaleUpCommand = ReactiveCommand.Create(() =>
    //     {
    //         int value = FilesInLine - 1;
    //         if (value < 1)
    //         {
    //             value = 1;
    //         }
    //
    //         FilesInLine = value;
    //     });
    // ScaleDownCommand = ReactiveCommand.Create(() =>
    // {
    //     int value = FilesInLine + 1;
    //     if (value > 12)
    //     {
    //         value = 12;
    //     }
    //
    //     FilesInLine = value;
    // });
}
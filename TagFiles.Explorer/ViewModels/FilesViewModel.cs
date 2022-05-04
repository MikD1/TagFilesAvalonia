using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using ReactiveUI;
using TagFiles.Explorer.Models;

namespace TagFiles.Explorer.ViewModels;

public class FilesViewModel : ViewModelBase
{
    public FilesViewModel(string location)
    {
        _viewMode = FilesViewMode.CreateList();
        _location = default!;
        _locationParts = default!;
        Nodes = new();

        SetLocation(location);
    }

    public FilesViewMode ViewMode
    {
        get => _viewMode;
        private set => this.RaiseAndSetIfChanged(ref _viewMode, value);
    }

    public ObservableCollection<FileNodeViewModel> Nodes { get; }

    public FileNodeViewModel? SelectedNode
    {
        get => _selectedNode;
        set
        {
            this.RaiseAndSetIfChanged(ref _selectedNode, value);
            if (value is not null)
            {
                SelectNode(value);
            }
        }
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
        ViewMode = FilesViewMode.CreateList();
    }

    public void SelectIconsMode()
    {
        ViewMode = FilesViewMode.CreateIcons();
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
        LocationParts = _location.Split(Path.DirectorySeparatorChar).Where(x => !string.IsNullOrEmpty(x)).ToArray();

        string? parent = GetParentLocation(_location);
        CanNavigateUp = parent != null;

        LoadNodes(_location);
    }

    private string? GetParentLocation(string location)
    {
        string? parent = Directory.GetParent(location)?.FullName;
        return parent;
    }

    private void LoadNodes(string location)
    {
        Nodes.Clear();
        SelectedNode = null;

        string[] directories = Directory.GetDirectories(location);
        string[] files = Directory.GetFiles(location);

        AddNodes(directories, FileNodeType.Directory);
        AddNodes(files, FileNodeType.File);
    }

    private void AddNodes(string[] items, FileNodeType type)
    {
        foreach (string item in items.OrderBy(x => x))
        {
            string name = Path.GetFileName(item);
            if (name.StartsWith('.'))
            {
                continue;
            }

            FileNode node = new(item, name, type);
            Nodes.Add(new FileNodeViewModel(node));
        }
    }

    private void SelectNode(FileNodeViewModel node)
    {
        if (node.Type.IsDirectory)
        {
            SetLocation(node.Path);
        }
    }

    private FilesViewMode _viewMode;
    private string _location;
    private string[] _locationParts;
    private bool _canNavigateUp;
    private FileNodeViewModel? _selectedNode;

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
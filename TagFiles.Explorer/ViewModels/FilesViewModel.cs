using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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
        _previewSize = 100;
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

    public int PreviewSize
    {
        get => _previewSize;
        private set => this.RaiseAndSetIfChanged(ref _previewSize, value);
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

    public void IncreasePreviewSize()
    {
        PreviewSize = Math.Min(PreviewSize + 5, 150);
    }

    public void DecreasePreviewSize()
    {
        PreviewSize = Math.Max(PreviewSize - 5, 10);
    }

    private async void SetLocation(string location)
    {
        _location = location;
        LocationParts = _location.Split(Path.DirectorySeparatorChar).Where(x => !string.IsNullOrEmpty(x)).ToArray();

        string? parent = GetParentLocation(_location);
        CanNavigateUp = parent != null;

        await LoadNodes(_location);
    }

    private string? GetParentLocation(string location)
    {
        string? parent = Directory.GetParent(location)?.FullName;
        return parent;
    }

    private async Task LoadNodes(string location)
    {
        Nodes.Clear();
        SelectedNode = null;

        string[] directories = Directory.GetDirectories(location);
        string[] files = Directory.GetFiles(location);

        AddNodes(directories, FileNodeType.Directory);
        AddNodes(files, FileNodeType.File);
        await LoadPreviews();
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

    private async Task LoadPreviews()
    {
        foreach (FileNodeViewModel node in Nodes.Where(x => x.Type.IsFile))
        {
            await node.LoadPreview();
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
    private int _previewSize;
    private FileNodeViewModel? _selectedNode;
}
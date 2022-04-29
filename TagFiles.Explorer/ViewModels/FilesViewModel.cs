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
        _location = location;
        Nodes = new();
        LoadNodes();
    }

    public string Location
    {
        get => _location;
        set => this.RaiseAndSetIfChanged(ref _location, value);
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

    private void SelectNode(FileNodeViewModel node)
    {
        if (node.Type.IsDirectory || node.Type.IsUpLink)
        {
            Location = node.Path;
            LoadNodes();
        }
    }

    private void LoadNodes()
    {
        Nodes.Clear();
        SelectedNode = null;

        string[] directories = Directory.GetDirectories(_location);
        string[] files = Directory.GetFiles(_location);

        AddParentIfExists();
        AddNodes(directories, FileNodeType.Directory);
        AddNodes(files, FileNodeType.File);
    }

    private void AddParentIfExists()
    {
        string? parent = Directory.GetParent(_location)?.FullName;
        if (parent != null)
        {
            FileNode node = new(parent, string.Empty, FileNodeType.UpLink);
            Nodes.Add(new FileNodeViewModel(node));
        }
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

    private string _location;
    private FileNodeViewModel? _selectedNode;
}
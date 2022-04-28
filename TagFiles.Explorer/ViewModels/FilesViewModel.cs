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

    public ObservableCollection<FileNode> Nodes { get; }

    public FileNode? SelectedNode
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

    public void SelectNode(FileNode node)
    {
        if (node.IsDirectory)
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
        AddNodes(directories, true);
        AddNodes(files, false);
    }

    private void AddParentIfExists()
    {
        string? parent = Directory.GetParent(_location)?.FullName;
        if (parent != null)
        {
            Nodes.Add(new FileNode(parent, "..", true));
        }
    }

    private void AddNodes(string[] items, bool isDirectory)
    {
        foreach (string item in items.OrderBy(x => x))
        {
            string name = Path.GetFileName(item);
            if (name.StartsWith('.'))
            {
                continue;
            }

            Nodes.Add(new FileNode(item, name, isDirectory));
        }
    }

    private string _location;
    private FileNode? _selectedNode;
}
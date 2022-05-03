using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using ReactiveUI;
using TagFiles.Explorer.Models;

namespace TagFiles.Explorer.ViewModels;

public class FilesListViewModel : ViewModelBase
{
    public FilesListViewModel(string location, Action<string> locationChanged)
    {
        _locationChanged = locationChanged;
        Nodes = new();
        LoadNodes(location);
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
        if (node.Type.IsDirectory)
        {
            _locationChanged(node.Path);
        }
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

    private readonly Action<string> _locationChanged;
    private FileNodeViewModel? _selectedNode;
}
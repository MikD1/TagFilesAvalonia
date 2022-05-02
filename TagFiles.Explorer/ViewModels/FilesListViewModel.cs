namespace TagFiles.Explorer.ViewModels;

public class FilesListViewModel : ViewModelBase
{
    // public FilesListViewModel(string location)
    // {
    //     _location = default!;
    //     _locationParts = default!;
    //
    //     Location = location;
    //     Nodes = new();
    //     LoadNodes();
    // }
    //
    // public string Location
    // {
    //     get => _location;
    //     set
    //     {
    //         this.RaiseAndSetIfChanged(ref _location, value);
    //         LocationParts = value.Split(Path.DirectorySeparatorChar).Where(x => !string.IsNullOrEmpty(x)).ToArray();
    //     }
    // }
    //
    // public string[] LocationParts
    // {
    //     get => _locationParts;
    //     set => this.RaiseAndSetIfChanged(ref _locationParts, value);
    // }
    //
    // public ObservableCollection<FileNodeViewModel> Nodes { get; }
    //
    // public FileNodeViewModel? SelectedNode
    // {
    //     get => _selectedNode;
    //     set
    //     {
    //         this.RaiseAndSetIfChanged(ref _selectedNode, value);
    //         if (value is not null)
    //         {
    //             SelectNode(value);
    //         }
    //     }
    // }
    //
    // private void SelectNode(FileNodeViewModel node)
    // {
    //     if (node.Type.IsDirectory || node.Type.IsUpLink)
    //     {
    //         Location = node.Path;
    //         LoadNodes();
    //     }
    // }
    //
    // private void LoadNodes()
    // {
    //     Nodes.Clear();
    //     SelectedNode = null;
    //
    //     string[] directories = Directory.GetDirectories(_location);
    //     string[] files = Directory.GetFiles(_location);
    //
    //     AddParentIfExists();
    //     AddNodes(directories, FileNodeType.Directory);
    //     AddNodes(files, FileNodeType.File);
    // }
    //
    // private void AddParentIfExists()
    // {
    //     string? parent = Directory.GetParent(_location)?.FullName;
    //     if (parent != null)
    //     {
    //         FileNode node = new(parent, string.Empty, FileNodeType.UpLink);
    //         Nodes.Add(new FileNodeViewModel(node));
    //     }
    // }
    //
    // private void AddNodes(string[] items, FileNodeType type)
    // {
    //     foreach (string item in items.OrderBy(x => x))
    //     {
    //         string name = Path.GetFileName(item);
    //         if (name.StartsWith('.'))
    //         {
    //             continue;
    //         }
    //
    //         FileNode node = new(item, name, type);
    //         Nodes.Add(new FileNodeViewModel(node));
    //     }
    // }
    //
    // private string _location;
    // private string[] _locationParts;
    // private FileNodeViewModel? _selectedNode;
}
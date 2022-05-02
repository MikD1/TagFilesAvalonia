namespace TagFiles.Explorer.ViewModels;

public class MainViewModel : ViewModelBase
{
    public MainViewModel(string location)
    {
        _location = location;
        Files = new FilesViewModel();
    }

    public FilesViewModel Files { get; }

    private string _location;
}
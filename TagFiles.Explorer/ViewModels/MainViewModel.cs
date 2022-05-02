namespace TagFiles.Explorer.ViewModels;

public class MainViewModel : ViewModelBase
{
    public MainViewModel(string location)
    {
        Files = new FilesViewModel(location);
    }

    public FilesViewModel Files { get; }
}
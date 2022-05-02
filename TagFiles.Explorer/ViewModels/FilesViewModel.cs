using ReactiveUI;

namespace TagFiles.Explorer.ViewModels;

public class FilesViewModel : ViewModelBase
{
    public FilesViewModel()
    {
        _content = new FilesListViewModel();
    }

    public ViewModelBase Content
    {
        get => _content;
        private set => this.RaiseAndSetIfChanged(ref _content, value);
    }

    public void SelectListMode()
    {
        Content = new FilesListViewModel();
    }

    public void SelectIconsMode()
    {
        Content = new FilesIconsViewModel();
    }

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
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia.Controls;
using ReactiveUI;
using TagFiles.Explorer.Models;
using TagFiles.Explorer.Views;

namespace TagFiles.Explorer.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            IsLoading = false;
            FilesInLine = 10;

            Tags = new ObservableCollection<Tag>();
            Tags.Add(new Tag("tag1", 28));
            Tags.Add(new Tag("a+b", 32));
            Tags.Add(new Tag("image", 11));
            Tags.Add(new Tag("media", 2));
            Tags.Add(new Tag("some_long_tag_name", 98));
            Tags.Add(new Tag("42", 34));
            Tags.Add(new Tag("12m", 102));

            LoadFilesCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                string? location = await RequestLocation();
            });
            ScaleUpCommand = ReactiveCommand.Create(() =>
            {
                int value = FilesInLine - 1;
                if (value < 1)
                {
                    value = 1;
                }

                FilesInLine = value;
            });
            ScaleDownCommand = ReactiveCommand.Create(() =>
            {
                int value = FilesInLine + 1;
                if (value > 12)
                {
                    value = 12;
                }

                FilesInLine = value;
            });
        }

        public bool IsLoading
        {
            get => _isLoading;
            set => this.RaiseAndSetIfChanged(ref _isLoading, value);
        }

        public ObservableCollection<Tag> Tags { get; }

        public ICommand LoadFilesCommand { get; }

        public ICommand ScaleUpCommand { get; }

        public ICommand ScaleDownCommand { get; }

        public FilesViewModel FilesViewModel { get; } = new(@"/Users/mik/Downloads");

        public int FilesInLine
        {
            get => _filesInLine;
            set => this.RaiseAndSetIfChanged(ref _filesInLine, value);
        }

        private async Task<string?> RequestLocation()
        {
            OpenFolderDialog dialog = new();
            return await dialog.ShowAsync(MainWindow.Instance!);
        }

        // private AppDbContext InitDatabase()
        // {
        //     string dbPath = System.IO.Path.Join(_location, "tagfiles.db");
        //     DbContextOptions<AppDbContext> options = new DbContextOptionsBuilder<AppDbContext>()
        //         .UseSqlite($"Data Source={dbPath}")
        //         .Options;
        //     AppDbContext dbContext = new AppDbContext(options);
        //     dbContext.Database.EnsureCreated();
        //     return dbContext;
        // }
        private bool _isLoading;
        private int _filesInLine;
    }
}
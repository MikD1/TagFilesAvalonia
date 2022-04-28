using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
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
            Files = new ObservableCollection<FileViewModel>();

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
                if (!string.IsNullOrEmpty(location))
                {
                    await LoadFiles(location);
                }
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

        public ObservableCollection<FileViewModel> Files { get; }

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

        private async Task LoadFiles(string location)
        {
            // IsLoading = true;
            Files.Clear();
            FilesManager filesManager = new();
            await filesManager.LoadFiles(location, fileInfo =>
            {
                Files.Add(new FileViewModel(fileInfo.Name, fileInfo.Format, new List<string>(),
                    CreateBitmap(fileInfo.Preview)));
            });

            // IsLoading = false;
        }

        private async Task<string?> RequestLocation()
        {
            OpenFolderDialog dialog = new();
            return await dialog.ShowAsync(MainWindow.Instance!);
        }

        private Bitmap? CreateBitmap(byte[]? bytes)
        {
            return bytes is null ? null : new Bitmap(new MemoryStream(bytes));
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
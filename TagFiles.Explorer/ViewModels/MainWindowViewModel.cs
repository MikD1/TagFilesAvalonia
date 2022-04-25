using System.Collections.Generic;
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
            FilesInLine = 5;
            Files = new ObservableCollection<FileViewModel>();
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

        public ICommand LoadFilesCommand { get; }

        public ICommand ScaleUpCommand { get; }

        public ICommand ScaleDownCommand { get; }

        public int FilesInLine
        {
            get => _filesInLine;
            set => this.RaiseAndSetIfChanged(ref _filesInLine, value);
        }

        private async Task LoadFiles(string location)
        {
            IsLoading = true;
            FilesManager filesManager = new();
            List<FileInfo> files = await filesManager.LoadFiles(location);

            Files.Clear();
            files.ForEach(x => Files.Add(new FileViewModel(x.Name, x.Format, new List<string>(), null)));
            IsLoading = false;
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
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using TagFiles.Explorer.ViewModels;
using TagFiles.Explorer.Views;

namespace TagFiles.Explorer
{
    // ReSharper disable once PartialTypeWithSinglePart
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow
                {
                    DataContext = new MainWindowViewModel()
                };
            }

            base.OnFrameworkInitializationCompleted();
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
    }
}
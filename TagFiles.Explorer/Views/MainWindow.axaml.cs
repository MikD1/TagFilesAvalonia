using Avalonia.ReactiveUI;
using TagFiles.Explorer.ViewModels;

namespace TagFiles.Explorer.Views
{
    public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
    {
        public static MainWindow? Instance { get; private set; }

        public MainWindow()
        {
            InitializeComponent();
            Instance = this;
        }
    }
}
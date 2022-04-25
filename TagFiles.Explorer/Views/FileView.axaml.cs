using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace TagFiles.Explorer.Views;

public partial class FileView : UserControl
{
    public FileView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}
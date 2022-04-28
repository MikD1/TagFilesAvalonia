using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace TagFiles.Explorer.Views;

// ReSharper disable once PartialTypeWithSinglePart
public partial class FilesView : UserControl
{
    public FilesView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}
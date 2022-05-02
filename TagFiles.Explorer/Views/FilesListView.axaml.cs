using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace TagFiles.Explorer.Views;

// ReSharper disable once PartialTypeWithSinglePart
public partial class FilesListView : UserControl
{
    public FilesListView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}
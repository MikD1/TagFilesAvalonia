using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace TagFiles.Explorer.Views;

public partial class TagsView : UserControl
{
    public TagsView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}
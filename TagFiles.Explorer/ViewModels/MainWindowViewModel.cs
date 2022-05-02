using System;
using DynamicData.Binding;
using ReactiveUI;

namespace TagFiles.Explorer.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            _content = CreateOpenFolderViewModel();
        }

        public ViewModelBase Content
        {
            get => _content;
            private set => this.RaiseAndSetIfChanged(ref _content, value);
        }

        private OpenFolderViewModel CreateOpenFolderViewModel()
        {
            OpenFolderViewModel viewModel = new OpenFolderViewModel();
            viewModel
                .WhenPropertyChanged(x => x.Folder)
                .Subscribe(property =>
                {
                    if (!string.IsNullOrEmpty(property.Value))
                    {
                        Content = CreateMainViewModel(property.Value);
                    }
                });

            return viewModel;
        }

        private MainViewModel CreateMainViewModel(string location)
        {
            return new MainViewModel(location);
        }

        private ViewModelBase _content;
    }
}
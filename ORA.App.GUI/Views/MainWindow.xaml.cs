using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ORA.App.GUI.ViewModels;
using ReactiveUI;

namespace ORA.App.GUI.Views
{
    public class MainWindow : ReactiveWindow<MainWindowViewModel>
    {
        public MainWindow()
        {
            this.InitializeComponent();
            ViewModel = new MainWindowViewModel();
            this.WhenActivated(disposables => {
                // this.OneWayBind(ViewModel, x => x.Router, x => x.RoutedViewHost.Router)
                //     .DisposeWith(disposables);
            });
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}

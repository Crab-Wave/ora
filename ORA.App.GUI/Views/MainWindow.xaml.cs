using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using ORA.App.GUI.ViewModels;

namespace ORA.App.GUI.Views
{
    public class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}

using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Interactivity;

using ORA.App.GUI.ViewModels;

namespace ORA.App.GUI.Views
{
    public class HomeView : UserControl
    {
        public HomeView()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void OnButtonClick(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Hey");
            var window = new MainWindow();
            window.Show();
        }
    }
}

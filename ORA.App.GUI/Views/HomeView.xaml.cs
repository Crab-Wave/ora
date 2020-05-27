using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Interactivity;

using ORA.App.GUI.ViewModels;
using ORA.App.GUI.Models;

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

        public void SelectCluster(object sender, RoutedEventArgs e)
        {
            (this.DataContext as HomeViewModel).SelectedCluster = (
                ((sender as ContentControl).Content as StackPanel).DataContext as ClusterItem).Cluster.Identifier;
        }
    }
}

using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace ORA.App.GUI.Views
{
    public class AddClusterView : UserControl
    {
        public AddClusterView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}

using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace ORA.App.GUI.Views
{
    public class AddClusterItemView : UserControl
    {
        public AddClusterItemView()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}

using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace ORA.App.GUI.Views
{
    public class AddFileView : UserControl
    {
        public AddFileView()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}

using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace ORA.App.GUI.Views
{
    public class AddMemberView : UserControl
    {
        public AddMemberView()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}

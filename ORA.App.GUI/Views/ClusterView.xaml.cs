using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace ORA.App.GUI.Views
{
    public class MemberView : UserControl
    {
        public MemberView()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}

using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using System.Reactive.Disposables;
using ReactiveUI;

using ORA.App.GUI.ViewModels;

namespace ORA.App.GUI.Views
{
    public class HomeView : ReactiveUserControl<HomeViewModel>
    {
        public HomeView()
        {
            InitializeComponent();
            this.WhenActivated(disposables => {
                // this.OneWayBind(ViewModel, x => x.UrlPathSegment)
                //     .DisposeWith(disposables);
            });
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}

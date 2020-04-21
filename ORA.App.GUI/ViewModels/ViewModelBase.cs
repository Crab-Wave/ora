using System;
using System.Reactive.Disposables;
using Avalonia;
using ReactiveUI;
using Avalonia.ReactiveUI;

namespace ORA.App.GUI.ViewModels
{
    public class ViewModelBase : ReactiveObject, IActivatableViewModel
    {
        public ViewModelActivator Activator { get; } = new ViewModelActivator();

        public ViewModelBase() => this.WhenActivated(
            (CompositeDisposable disposables) =>
            {
                Disposable
                    .Create(() => { })
                    .DisposeWith(disposables);
            }
        );
    }
}

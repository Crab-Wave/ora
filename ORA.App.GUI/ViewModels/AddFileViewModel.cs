using System.Reactive;
using ReactiveUI;
using System;
using ORA.API;
using ORA.App.GUI.Models;

namespace ORA.App.GUI.ViewModels
{
    internal class AddFileViewModel : ViewModelBase
    {
        private string name;
        public string Name
        {
            get => this.name;
            set => this.RaiseAndSetIfChanged(ref this.name, value);
        }

        public ReactiveCommand<Unit, ClusterItem> Ok { get; }
        public ReactiveCommand<Unit, Unit> Cancel { get; }

        public MainWindowViewModel MainWindowViewModel { get; set; }

        public AddFileViewModel()
        {
            var okEnabled = this.WhenAnyValue(
                x => x.Name,
                x => !String.IsNullOrWhiteSpace(x)
            );

            this.Ok = ReactiveCommand.Create(
                () => new ClusterItem(this.MainWindowViewModel, Ora.GetClusterManager().CreateCluster(this.name, "displayName")),
                okEnabled);
            this.Cancel = ReactiveCommand.Create(() => { });
        }
    }
}

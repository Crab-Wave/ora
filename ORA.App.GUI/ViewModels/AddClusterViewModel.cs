using System.Reactive;
using ReactiveUI;
using System;
using ORA.API;
using ORA.App.GUI.Models;

namespace ORA.App.GUI.ViewModels
{
    internal class AddClusterViewModel : ViewModelBase
    {
        private string name;
        public string Name
        {
            get => this.name;
            set => this.RaiseAndSetIfChanged(ref this.name, value);
        }

        private string id;
        public string Id
        {
            get => this.id;
            set => this.RaiseAndSetIfChanged(ref this.id, value);
        }

        public ReactiveCommand<Unit, ClusterItem> Create { get; }
        public ReactiveCommand<Unit, ClusterItem> Join { get; }
        public ReactiveCommand<Unit, Unit> Cancel { get; }

        public MainWindowViewModel MainWindowViewModel { get; set; }

        public AddClusterViewModel(string username)
        {
            var createEnabled = this.WhenAnyValue(
                x => x.Name,
                x => !String.IsNullOrWhiteSpace(x)
            );
            var joinEnabled = this.WhenAnyValue(
                x => x.Id,
                x => !String.IsNullOrWhiteSpace(x)
            );

            this.Create = ReactiveCommand.Create(
                () => new ClusterItem(this.MainWindowViewModel, Ora.GetClusterManager().CreateCluster(this.name, username)),
                createEnabled);

            this.Join = ReactiveCommand.Create(
                () =>
                {
                    bool isJoined = Ora.GetClusterManager().JoinCluster(this.id, username);
                    return new ClusterItem(this.MainWindowViewModel, isJoined ? Ora.GetClusterManager().GetCluster(this.id) : null);
                },
                joinEnabled);

            this.Cancel = ReactiveCommand.Create(() => { });
        }
    }
}

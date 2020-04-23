using System.Reactive;
using ReactiveUI;

using ORA.API;

namespace ORA.App.GUI.ViewModels
{
    class AddClusterViewModel : ViewModelBase
    {
        private string name;
        public string Name
        {
            get => name;
            set => this.RaiseAndSetIfChanged(ref name, value);
        }

        public ReactiveCommand<Unit, Cluster> Ok { get; }
        public ReactiveCommand<Unit, Unit> Cancel { get; }

        public AddClusterViewModel()
        {
            var okEnabled = this.WhenAnyValue(
                x => x.Name,
                x => !string.IsNullOrWhiteSpace(x)
            );

            Ok = ReactiveCommand.Create(
                () => Ora.GetClusterManager().CreateCluster(name),
                okEnabled);
            Cancel = ReactiveCommand.Create(() => { });
        }
    }
}

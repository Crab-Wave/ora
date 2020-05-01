using System.Reactive;
using ReactiveUI;

using ORA.API;
using ORA.App.GUI.Models;

namespace ORA.App.GUI.ViewModels
{
    class AddClusterItemViewModel : ViewModelBase
    {
        private string name;
        public string Name
        {
            get => name;
            set => this.RaiseAndSetIfChanged(ref name, value);
        }

        public ReactiveCommand<Unit, ClusterItem> Ok { get; }
        public ReactiveCommand<Unit, Unit> Cancel { get; }

        public MainWindowViewModel MainWindowViewModel { get; set; }

        public AddClusterItemViewModel()
        {
            var okEnabled = this.WhenAnyValue(
                x => x.Name,
                x => !string.IsNullOrWhiteSpace(x)
            );

            Ok = ReactiveCommand.Create(
                () => new ClusterItem(MainWindowViewModel, Ora.GetClusterManager().CreateCluster(name, "displayName")),
                okEnabled);
            Cancel = ReactiveCommand.Create(() => { });
        }
    }
}

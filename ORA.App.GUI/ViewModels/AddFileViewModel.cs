using System.Reactive;
using ReactiveUI;
using System;
using ORA.API;
using ORA.App.GUI.Models;

namespace ORA.App.GUI.ViewModels
{
    internal class AddFileViewModel : ViewModelBase
    {
        private string realPath;
        private string clusterPath;

        public string RealPath
        {
            get => this.realPath;
            set => this.RaiseAndSetIfChanged(ref this.realPath, value);
        }

        public string ClusterPath
        {
            get => this.clusterPath;
            set => this.RaiseAndSetIfChanged(ref this.clusterPath, value);
        }

        public ReactiveCommand<Unit, FileItem> Ok { get; }
        public ReactiveCommand<Unit, Unit> Cancel { get; }

        public ClusterViewModel ClusterViewModel { get; set; }

        public AddFileViewModel()
        {
            var okEnabled = this.WhenAnyValue(
                x => x.RealPath,
                x => !String.IsNullOrWhiteSpace(x)
            );
            // .WhenAnyValue(
            //     x => x.ClusterPath,
            //     x => !String.IsNullOrWhiteSpace(x)
            // );

            this.Ok = ReactiveCommand.Create(
                () => new FileItem(this.ClusterViewModel, Ora.GetFileManager().CreateFile(
                    this.ClusterViewModel.Cluster, this.realPath, this.clusterPath)),
                okEnabled);
            this.Cancel = ReactiveCommand.Create(() => { });
        }
    }
}

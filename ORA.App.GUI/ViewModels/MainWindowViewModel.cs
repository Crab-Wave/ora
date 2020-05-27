using System;
using System.Reactive.Linq;
using System.Collections.Generic;
using System.Linq;
using ReactiveUI;
using ORA.API;
using ORA.App.GUI.Models;

namespace ORA.App.GUI.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ViewModelBase content;

        public ViewModelBase Content
        {
            get => this.content;
            set => this.RaiseAndSetIfChanged(ref this.content, value);
        }

        public HomeViewModel Home { get; set; }
        public SettingsViewModel Settings { get; }
        public ClusterViewModel Cluster { get; set; }

        public MainWindowViewModel()
        {
            this.Content = this.Home =
                new HomeViewModel(Ora.GetClusterManager().GetClustersOfUser(
                    Ora.GetIdentityManager().GetIdentity().GetIdentifier()
                ).Select(cluster => new ClusterItem(this, cluster)));
            this.Settings = new SettingsViewModel();
        }

        public void NavigateToHome()
        {
            this.Content = this.Home;
        }

        public void NavigateToSettings()
        {
            this.Content = this.Settings;
        }

        public void NavigateToCluster()
        {
            this.Content = this.Cluster;
        }

        public void AddMemberItem()
        {

        }
        public void AddClusterItem()
        {
            var vm = new AddClusterItemViewModel();

            Observable.Merge(
                    vm.Ok,
                    vm.Cancel.Select(_ => (ClusterItem) null))
                .Take(1)
                .Subscribe(model =>
                {
                    if (model != null)
                    {
                        this.Home.Clusters.Add(model);
                    }

                    this.Content = this.Home;
                });

            this.Content = vm;
        }
    }
}

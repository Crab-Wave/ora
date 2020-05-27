using System;
using System.Reactive.Linq;
using System.Collections.Generic;
using System.Linq;
using ReactiveUI;
using Avalonia.Interactivity;
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
            this.Cluster = new ClusterViewModel(Ora.GetClusterManager().GetCluster(this.Home.SelectedCluster));
            this.Content = this.Cluster;
        }

        public void AddClusterItem()
        {
            var vm = new AddClusterViewModel(this.Settings.Username);

            Observable.Merge(
                    vm.Create,
                    vm.Join,
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

        public void InviteMember()
        {
            var vm = new AddMemberViewModel(this.Cluster.Cluster.Identifier);

            Observable.Merge(
                    vm.Invite,
                    vm.Cancel.Select(_ => false))
                .Take(1)
                .Subscribe(model =>
                {
                    this.Content = this.Cluster;
                });

            this.Content = vm;
        }

        public void AddFileItem()
        {
            var vm = new AddFileViewModel();

            Observable.Merge(
                    vm.Ok,
                    vm.Cancel.Select(_ => (FileItem) null))
                .Take(1)
                .Subscribe(model =>
                {
                    if (model != null)
                    {
                        this.Cluster.Files.Add(model);
                    }

                    this.Content = this.Cluster;
                });

            this.Content = vm;
        }
    }
}

using System;
using System.Reactive.Linq;
using System.Collections.Generic;
using ReactiveUI;

using ORA.API;

namespace ORA.App.GUI.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ViewModelBase content;
        public ViewModelBase Content
        {
            get => content;
            private set => this.RaiseAndSetIfChanged(ref content, value);
        }

        public HomeViewModel Home { get; }
        public SettingsViewModel Settings { get; }

        public IEnumerable<Cluster> GetClusters() => new[]
        {
            Ora.GetClusterManager().GetCluster("70e69ee5-920b-4a6d-888d-fec75dea738a"),
            Ora.GetClusterManager().GetCluster("71fc8375-dfbe-4f10-b379-cf0590866463"),
            Ora.GetClusterManager().GetCluster("74b4ab16-98a7-4d20-a84b-3ef72320cee5")
        };

        public MainWindowViewModel()
        {
            Content = Home = new HomeViewModel(this.GetClusters());
            Settings = new SettingsViewModel();
        }

        public void NavigateToHome()
        {
            Content = Home;
        }

        public void NavigateToSettings()
        {
            Content = Settings;
        }

        public void AddCluster()
        {
            var vm = new AddClusterViewModel();

            Observable.Merge(
                vm.Ok,
                vm.Cancel.Select(_ => (Cluster)null))
                .Take(1)
                .Subscribe(model =>
                {
                    if (model != null)
                    {
                        Home.Clusters.Add(model);
                    }

                    Content = Home;
                });

            Content = vm;
        }
    }
}

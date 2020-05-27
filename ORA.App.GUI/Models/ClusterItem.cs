using System.Linq;
using ORA.API;
using ORA.App.GUI.ViewModels;
using System.Collections.Generic;

namespace ORA.App.GUI.Models
{
    public class ClusterItem
    {
        public Cluster Cluster { get; set; }
        public string Information { get => $"{this.Cluster.Name} {this.Cluster.Identifier}"; }

        private MainWindowViewModel MainWindowViewModel;

        public ClusterItem(MainWindowViewModel mainWindowViewModel, Cluster cluster)
        {
            this.Cluster = cluster;
            this.MainWindowViewModel = mainWindowViewModel;
        }

        public void Remove()
        {
            Ora.GetClusterManager().DeleteCluster(this.Cluster.Identifier);
            this.MainWindowViewModel.Content = this.MainWindowViewModel.Home =
                new HomeViewModel(Ora.GetClusterManager().GetClusters().Select(c => new ClusterItem(this.MainWindowViewModel, c)));
        }
    }
}

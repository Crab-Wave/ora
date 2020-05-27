using System.Linq;
using System.Collections.Generic;

using ORA.API;
using ORA.App.GUI.ViewModels;

namespace ORA.App.GUI.Models
{
    public class ClusterItem
    {
        public Cluster Cluster { get; }

        private MainWindowViewModel mainWindowViewModel;

        public ClusterItem(MainWindowViewModel mainWindowViewModel, Cluster cluster)
        {
            this.mainWindowViewModel = mainWindowViewModel;
            this.Cluster = cluster;
        }

        public void Remove()
        {
            Ora.GetClusterManager().DeleteCluster(this.Cluster.Identifier);
            this.mainWindowViewModel.Content = this.mainWindowViewModel.Home =
                new HomeViewModel(Ora.GetClusterManager().GetClusters().Select(c => new ClusterItem(this.mainWindowViewModel, c)));
        }
    }
}

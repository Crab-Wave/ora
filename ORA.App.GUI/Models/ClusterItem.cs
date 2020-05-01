using System.Linq;
using ORA.API;
using ORA.App.GUI.ViewModels;

namespace ORA.App.GUI.Models
{
    public class ClusterItem
    {
        public Cluster cluster { get; set; }
        public string Information { get => $"{this.cluster.Name} {this.cluster.Identifier}"; }

        private MainWindowViewModel MainWindowViewModel;

        public ClusterItem(MainWindowViewModel mainWindowViewModel, Cluster cluster)
        {
            this.cluster = cluster;
            this.MainWindowViewModel = mainWindowViewModel;
        }

        public void Remove()
        {
            Ora.GetClusterManager().DeleteCluster(this.cluster.Identifier);
            this.MainWindowViewModel.Content = this.MainWindowViewModel.Home =
                new HomeViewModel(Ora.GetClusterManager().GetClusters().Select(c => new ClusterItem(this.MainWindowViewModel, c)));
        }
    }
}

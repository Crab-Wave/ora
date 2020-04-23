using ORA.API;

namespace ORA.App.GUI.Models
{
    public class ClusterItem
    {
        public Cluster cluster { get; set; }
        public string Information { get => $"{this.cluster.Name} {this.cluster.Identifier}"; }

        public ClusterItem(Cluster cluster)
        {
            this.cluster = cluster;
        }
    }
}

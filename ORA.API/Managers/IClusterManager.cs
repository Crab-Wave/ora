namespace ORA.API.Managers
{
    public interface IClusterManager
    {
        Cluster CreateCluster(string name);

        Cluster GetCluster(string identifier);

        bool DeleteCluster(string identifier);
    }
}

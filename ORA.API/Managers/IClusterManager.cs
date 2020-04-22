namespace ORA.API.Managers
{
    /// <summary>
    ///     An interface representing the management of the Clusters through many methods such as CreateCluster, GetCluster and DeleteCluster.
    /// </summary>
    public interface IClusterManager
    {
        /// <summary>
        ///     Create a cluster with the specified name
        /// </summary>
        /// <param name="name">The name of the cluster</param>
        /// <returns>The created cluster</returns>
        Cluster CreateCluster(string name);

        /// <summary>
        ///    Get the cluster with the specified identifier.
        /// </summary>
        /// <param name="identifier">The identifier of the cluster wanted</param>
        /// <returns>The cluster which has the specified identifier</returns>
        Cluster GetCluster(string identifier);

        /// <summary>
        ///     Delete the cluster with the specified identifier.
        /// </summary>
        /// <param name="identifier">The identifier of the cluster wanted</param>
        /// <returns>True if the deletion has succeeded or false otherwise.</returns>
        bool DeleteCluster(string identifier);
    }
}

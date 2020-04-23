using System.Collections.Generic;

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
        /// <param name="userDisplayName">The display name of the user creating the cluster</param>
        /// <returns>The created cluster</returns>
        Cluster CreateCluster(string name, string userDisplayName);

        /// <summary>
        ///    Get the list of all the clusters
        /// </summary>
        /// <returns>The list of all the clusters</returns>
        List<Cluster> GetClusters();

        /// <summary>
        ///    Get the cluster with the specified identifier
        /// </summary>
        /// <param name="cluster">The identifier of the cluster</param>
        /// <returns>The cluster which has the specified identifier</returns>
        Cluster GetCluster(string cluster);

        /// <summary>
        ///     Delete the cluster with the specified identifier
        /// </summary>
        /// <param name="cluster">The identifier of the cluster</param>
        /// <returns>True if the deletion has succeeded or false otherwise.</returns>
        bool DeleteCluster(string cluster);

        /// <summary>
        ///     Get the list of the members of the specified cluster
        /// </summary>
        /// <param name="cluster">The identifier of the cluster</param>
        /// <returns>The list of the members of the current cluster </returns>
        public List<Member> GetMembers(string cluster);

        /// <summary>
        ///     Get the member with the specified identifier from the specified cluster
        /// </summary>
        /// <param name="cluster">The identifier of the cluster</param>
        /// <param name="member">The member identifier</param>
        /// <returns>The member in the specified cluster with the specified identifier</returns>
        public Member GetMember(string cluster, string member);

        /// <summary>
        ///     Remove the member with the specified identifier from the specified cluster
        /// </summary>
        /// <param name="cluster">The identifier of the cluster</param>
        /// <param name="member">The member identifier</param>
        /// <returns>True if the member has been removed or false otherwise</returns>
        public bool RemoveMember(string cluster, string member);
    }
}

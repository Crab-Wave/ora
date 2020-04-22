using System.Collections.Generic;

namespace ORA.API
{
    /// <summary>
    /// A cluster is a group where members which own Nodes will be able to share their files and their directories.
    /// </summary>
    public abstract class Cluster
    {
        /// <summary>
        /// A cluster has 2 attributes : a name and an identifier
        /// </summary>
        private string _name;
        private string _identifier;

        /// <summary>
        /// There is a getter for both of those.
        /// </summary>

        public string Name => this._name;
        public string Identifier => this._identifier;

        /// <summary>
        ///Initializes a new cluster
        /// </summary>
        /// <param name="name">the name of the new cluster </param>
        /// <param name="identifier">the identifier of the new cluster</param>

        protected Cluster(string name, string identifier)
        {
            this._name = name;
            this._identifier = identifier;
        }

        /// <summary>
        /// Get the list of the members of the current cluster
        /// </summary>
        /// <returns>the list of the memebers of the current cluster </returns>
        public abstract List<Member> GetMembers();

        /// <summary>
        /// Get the member of the current cluster with the specified identifier
        /// </summary>
        /// <param name="identifier">the specified identifier</param>
        /// <returns>the member in the current cluster with the identifier specified</returns>
        public abstract Member GetMember(string identifier);

        /// <summary>
        /// Remove a member of a cluster with the identifier specified
        /// </summary>
        /// <param name="identifier">the specified identifier</param>
        /// <returns>True if the member has been removed or false otherwise</returns>
        public abstract bool RemoveMember(string identifier);
    }
}

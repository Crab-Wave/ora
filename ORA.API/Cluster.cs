namespace ORA.API
{
    /// <summary>
    /// A cluster is a group where members which own Nodes will be able to share their files and their directories.
    /// </summary>
    public class Cluster
    {
        /// <summary>
        ///     The cluster name
        /// </summary>
        public string Name { get; }

        /// <summary>
        ///     The cluster identifier
        /// </summary>
        public string Identifier { get; }

        /// <summary>
        ///     The identifier of the cluster owner
        /// </summary>
        public string Owner { get; }


        /// <summary>
        /// Initializes a new cluster
        /// </summary>
        /// <param name="name">The name of the new cluster </param>
        /// <param name="identifier">The identifier of the new cluster</param>
        /// <param name="owner">The identifier of the cluster owner</param>
        public Cluster(string name, string identifier, string owner)
        {
            this.Name = name;
            this.Identifier = identifier;
            this.Owner = owner;
        }

        public override string ToString() => $"Cluster[name={this.Name}, id={this.Identifier}, owner={this.Owner}]";
    }
}

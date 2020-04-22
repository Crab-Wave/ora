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
        /// Initializes a new cluster
        /// </summary>
        /// <param name="name">the name of the new cluster </param>
        /// <param name="identifier">the identifier of the new cluster</param>
        public Cluster(string name, string identifier)
        {
            this.Name = name;
            this.Identifier = identifier;
        }
    }
}

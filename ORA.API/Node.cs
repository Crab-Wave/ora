namespace ORA.API
{
    /// <summary>
    /// A node is a device used by a member which provides the files and the directories to share.
    /// </summary>
    public class Node
    {
        /// <summary>
        ///     The node name
        /// </summary>
        public string Name { get; }

        /// <summary>
        ///     The node identifier
        /// </summary>
        public string Identifier { get; }

        /// <summary>
        /// Initializes a new node.
        /// </summary>
        /// <param name="name">the name of the new node</param>
        /// <param name="identifier">the identifier of the new node</param>
        public Node(string name, string identifier)
        {
            this.Name = name;
            this.Identifier = identifier;
        }
    }
}

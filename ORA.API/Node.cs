namespace ORA.API
{
    /// <summary>
    /// A node is a device used by a member which provides the files and the directories to share.
    /// </summary>
    public abstract class Node
    {
        /// <summary>
        /// A node has two attributes : a name and an identifier.
        /// </summary>
        private string _name;
        private string _identifier;

        /// <summary>
        /// Those two attributes have both a getter.
        /// </summary>
        public string Name => this._name;
        public string Identifier => this._identifier;

        /// <summary>
        /// Initializes a new node.
        /// </summary>
        /// <param name="name">the name of the new node</param>
        /// <param name="identifier">the identifier of the new node</param>
        protected Node(string name, string identifier)
        {
            this._name = name;
            this._identifier = identifier;
        }
    }
}

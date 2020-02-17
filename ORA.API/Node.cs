namespace ORA.API
{
    public abstract class Node
    {
        private string _name;
        private string _identifier;

        public string Name => this._name;
        public string Identifier => this._identifier;

        protected Node(string name, string identifier)
        {
            this._name = name;
            this._identifier = identifier;
        }
    }
}

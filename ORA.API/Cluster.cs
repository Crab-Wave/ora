using System.Collections.Generic;

namespace ORA.API
{
    public abstract class Cluster
    {
        private string _name;
        private string _identifier;

        public string Name => this._name;
        public string Identifier => this._identifier;

        protected Cluster(string name, string identifier)
        {
            this._name = name;
            this._identifier = identifier;
        }

        public abstract List<T> GetNodes<T>() where T : Node;

        public abstract T GetNode<T>(string identifier) where T : Node;

        public abstract void AddNode<T>(T node) where T : Node;

        public abstract void RemoveNode(string identifier);
    }
}

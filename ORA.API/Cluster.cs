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

        public abstract List<Member> GetMembers();

        public abstract Member GetMember(string identifier);

        public abstract bool RemoveMemeber(string identifier);
    }
}

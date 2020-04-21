namespace ORA.API
{
    public class Member
    {
        private string _identifier;
        private string _name;

        public string Name => this._name;
        public string Identifier => this._identifier;

        protected Member(string identifier, string name)
        {
            this._identifier = identifier;
            this._name = name;
        }
    }
}

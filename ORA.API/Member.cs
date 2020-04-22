namespace ORA.API
{
    /// <summary>
    /// A member represents a user in a cluster, with an identifier pointing to his identity and a display name.
    /// </summary>
    public class Member
    {
        /// <summary>
        /// A member has two attributes : a name and an identifier.
        /// </summary>
        private string _identifier;

        private string _name;

        /// <summary>
        /// These two attibutes have both a getter.
        /// </summary>
        public string Name => this._name;

        public string Identifier => this._identifier;

        /// <summary>
        /// Initializes a new member.
        /// </summary>
        /// <param name="identifier">the identifier of the new member</param>
        /// <param name="name">the name of the new member</param>
        protected Member(string identifier, string name)
        {
            this._identifier = identifier;
            this._name = name;
        }
    }
}

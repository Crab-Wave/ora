namespace ORA.API.Managers
{
    /// <summary>
    ///     An interface representing the management of the Identities through many methods such as GetIdentity and GenerateIdentity.
    /// </summary>
    public interface IIdentityManager
    {
        /// <summary>
        ///    Get the identity of the current user.
        /// </summary>
        /// <returns>The identity wanted</returns>
        Identity GetIdentity();

        /// <summary>
        ///     Generate a new identity with the name specified.
        /// </summary>
        /// <param name="name">The name of the new identity. </param>
        /// <returns>The created identity</returns>
        Identity GenerateIdentity(string name);
    }
}

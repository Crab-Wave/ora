namespace ORA.API.Managers
{
    /// <summary>
    ///     An interface representing the management of the the Authentication and the Authorization.
    /// </summary>
    public interface IAuthManager
    {
        /// <summary>
        ///     Authenticate the user with the tracker
        /// </summary>
        /// <returns>The auth token</returns>
        public string Authenticate();

        /// <summary>
        ///     Returns the current auth token
        /// </summary>
        /// <returns>The auth token</returns>
        public string GetToken();

        /// <summary>
        ///     Refreshes the current auth token
        /// </summary>
        /// <returns>The auth token</returns>
        public string RefreshToken();

        /// <summary>
        ///     Returns whether the application is authenticated or not
        /// </summary>
        /// <returns>Whether the application is authenticated or not</returns>
        public bool IsAuthenticated();

        public void Disconnect();
    }
}

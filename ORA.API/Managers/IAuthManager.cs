namespace ORA.API.Managers
{
    public interface IAuthManager
    {
        public string Authenticate();

        public string GetToken();

        public string RefreshToken();

        public bool IsAuthenticated();
    }
}

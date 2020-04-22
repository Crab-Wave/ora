namespace ORA.API.Managers
{
    public interface IIdentityManager
    {
        Identity GetIdentity();

        Identity GenerateIdentity(string name);
    }
}

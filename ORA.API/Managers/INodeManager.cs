namespace ORA.API.Managers
{
    public interface INodeManager
    {
        void Initialize();

        string GetIp();

        void RefreshIp();
    }
}

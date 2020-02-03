namespace ORA.API.Managers
{
    public interface INodeManager
    {
        Node CreateNode(string name);

        Node GetNode(string identifier);

        bool DeleteNode(string identifier);
    }
}

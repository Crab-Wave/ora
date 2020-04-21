namespace ORA.API.Managers
{
    public interface IMemberManager
    {
        Member[] GetMembers();
        Member GetMember(string identifier);
        bool RemoveMember(string identifier);
    }
}

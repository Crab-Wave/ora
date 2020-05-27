using ORA.API.Network;

namespace ORA.API.Managers
{
    public interface INetworkManager
    {
        Packet SendPacket(string host, int port, Packet packet);

        void AddPacketHandler(PacketHandler packetHandler);
    }
}

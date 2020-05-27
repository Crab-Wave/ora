using JKang.IpcServiceFramework;
using ORA.API;
using ORA.API.Managers;
using ORA.API.Network;

namespace ORA.Core.IPC.Managers
{
    public class IpcNetworkManager : INetworkManager
    {
        private IpcServiceClient<INetworkManager> _client;

        public IpcNetworkManager(IpcServiceClient<INetworkManager> client)
        {
            this._client = client;
        }

        public Packet SendPacket(string host, int port, Packet packet) =>
            this._client.InvokeAsync(manager => manager.SendPacket(host, port, packet)).Result;

        public void AddPacketHandler(PacketHandler packetHandler) =>
            this._client.InvokeAsync(manager => manager.AddPacketHandler(packetHandler)).Wait();
    }
}

using System;

namespace ORA.API.Network
{
    public interface PacketHandler
    {
        void OnPacketReceive(Packet packet);

        Type Type();
    }
}

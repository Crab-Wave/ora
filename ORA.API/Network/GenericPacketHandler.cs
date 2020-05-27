using System;

namespace ORA.API.Network
{
    public abstract class GenericPacketHandler<T> : PacketHandler where T : Packet
    {
        public abstract void GenericOnPacketReceive(T packet);

        public void OnPacketReceive(Packet packet) => this.GenericOnPacketReceive((T) packet);

        public Type Type() => typeof(T);
    }
}

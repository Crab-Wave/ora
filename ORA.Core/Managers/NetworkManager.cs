using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using ORA.API.Managers;
using ORA.API.Network;
using ORA.API.Network.Packets;

namespace ORA.Core.Managers
{
    public class NetworkManager : INetworkManager
    {
        private List<PacketHandler> PacketHandlers;
        private Dictionary<int, Type> Packets;
        private TcpListener TcpListener;

        public NetworkManager()
        {
            this.PacketHandlers = new List<PacketHandler>();
            this.Packets = new Dictionary<int, Type>();
            this.Packets.Add(0, typeof(RequestFilePacket));
            this.Packets.Add(1, typeof(FilePacket));
        }

        public void StartListening()
        {
            if (this.TcpListener != null)
                return;
            this.TcpListener = new TcpListener(5000);
            this.TcpListener.Start();
            new Thread(this.Run).Start();
        }

        public void Stop()
        {
            if (this.TcpListener == null)
                return;
            this.TcpListener.Stop();
            this.TcpListener = null;
        }

        private void Run()
        {
            while (this.TcpListener != null)
            {
                TcpClient client = this.TcpListener.AcceptTcpClient();

                NetworkStream stream = client.GetStream();

                Packet packet = this.ReadPacket(stream);

                foreach (PacketHandler packetHandler in this.PacketHandlers)
                    if (packetHandler.Type() == packet.GetType())
                        packetHandler.OnPacketReceive(packet);

                if (packet.NeedResponse)
                    this.WritePacket(packet.Respond(), stream);

                client.Close();
            }
        }

        private Packet ReadPacket(NetworkStream networkStream)
        {
            byte[] packetIdBytes = new byte[4];
            networkStream.Read(packetIdBytes);
            int packetId = BitConverter.ToInt32(packetIdBytes);
            Type type = this.Packets[packetId];
            Packet packet = (Packet) type.GetConstructor(new Type[0]).Invoke(null);

            MemoryStream memoryStream = new MemoryStream();

            int i;
            byte[] bytes = new byte[256];
            while ((i = networkStream.Read(bytes, 0, bytes.Length)) != 0)
                memoryStream.Write(bytes);

            packet.Deserialize(memoryStream.ToArray());

            return packet;
        }

        private void WritePacket(Packet packet, NetworkStream networkStream)
        {
            byte[] data = packet.Serialize();
            networkStream.Write(BitConverter.GetBytes(packet.Id));
            networkStream.Write(data);
        }

        public Packet SendPacket(string host, int port, Packet packet)
        {
            TcpClient client = new TcpClient(host, port);

            NetworkStream stream = client.GetStream();

            this.WritePacket(packet, stream);

            Packet response = null;
            if (packet.NeedResponse)
                response = this.ReadPacket(stream);

            stream.Close();
            client.Close();

            return response;
        }

        public void AddPacketHandler(PacketHandler packetHandler) =>
            this.PacketHandlers.Add(packetHandler);
    }
}

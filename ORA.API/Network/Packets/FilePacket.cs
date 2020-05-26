using System;
using System.IO;
using System.Text;

namespace ORA.API.Network.Packets
{
    public class FilePacket : Packet
    {
        private string cluster;
        private string hash;
        private byte[] content;

        public string Cluster => this.cluster;

        public string Hash => this.hash;

        public byte[] Content => this.content;

        public FilePacket(string cluster, string hash, byte[] content) : base(1)
        {
            this.cluster = cluster;
            this.hash = hash;
            this.content = content;
        }

        public FilePacket() : base(1)
        {
        }

        public override byte[] Serialize()
        {
            MemoryStream stream = new MemoryStream();

            byte[] cluster = Encoding.ASCII.GetBytes(this.cluster);
            stream.Write(BitConverter.GetBytes(cluster.Length));
            stream.Write(cluster);

            byte[] hash = Encoding.ASCII.GetBytes(this.hash);
            stream.Write(BitConverter.GetBytes(hash.Length));
            stream.Write(hash);

            stream.Write(BitConverter.GetBytes(this.content.Length));
            stream.Write(this.content);

            byte[] data = stream.ToArray();

            stream.Close();

            return data;
        }

        public override void Deserialize(byte[] data)
        {
            MemoryStream stream = new MemoryStream(data);

            byte[] clusterLengthBytes = new byte[4];
            stream.Read(clusterLengthBytes);
            int clusterLength = BitConverter.ToInt32(clusterLengthBytes);
            byte[] cluster = new byte[clusterLength];
            stream.Read(cluster);
            this.cluster = Encoding.ASCII.GetString(cluster);

            byte[] hashLengthBytes = new byte[4];
            stream.Read(hashLengthBytes);
            int hashLength = BitConverter.ToInt32(hashLengthBytes);
            byte[] hash = new byte[hashLength];
            stream.Read(hash);
            this.hash = Encoding.ASCII.GetString(hash);

            byte[] contentLengthBytes = new byte[4];
            stream.Read(contentLengthBytes);
            int contentLength = BitConverter.ToInt32(contentLengthBytes);
            this.content = new byte[contentLength];
            stream.Read(this.content);

            stream.Close();
        }
    }
}

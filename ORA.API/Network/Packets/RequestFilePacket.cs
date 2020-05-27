using System;
using System.IO;
using System.Text;

namespace ORA.API.Network.Packets
{
    public class RequestFilePacket : Packet
    {
        private string cluster;
        private string hash;

        public string Cluster => this.cluster;

        public string Hash => this.hash;

        public RequestFilePacket(string cluster, string hash) : base(0, true)
        {
            this.cluster = cluster;
            this.hash = hash;
        }

        public RequestFilePacket() : base(0, true)
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

            stream.Close();
        }

        public override Packet Respond() =>
            new FilePacket(this.cluster, this.hash,
                Ora.GetFileManager().GetFileContent(Ora.GetClusterManager().GetCluster(this.cluster), this.hash));
    }
}

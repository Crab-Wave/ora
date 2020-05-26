namespace ORA.API.Network
{
    public abstract class Packet
    {
        public readonly int Id;
        public readonly bool NeedResponse;

        protected Packet(int id) : this(id, false)
        {
        }

        protected Packet(int id, bool needResponse)
        {
            this.Id = id;
            this.NeedResponse = needResponse;
        }

        public abstract byte[] Serialize();

        public abstract void Deserialize(byte[] data);

        public virtual Packet Respond() => null;
    }
}

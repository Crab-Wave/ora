namespace ORA.API
{
    public class Identity
    {
        private byte[] _publicKey;
        private byte[] _privateKey;

        public byte[] PublicKey => this._publicKey;
        public byte[] PrivateKey => this._privateKey;

        public Identity(byte[] publicKey, byte[] privateKey)
        {
            this._publicKey = publicKey;
            this._privateKey = privateKey;
        }
    }
}

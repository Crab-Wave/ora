namespace ORA.API
{
    /// <summary>
    /// An identity represents a user
    /// </summary>
    public class Identity
    {
        /// <summary>
        /// An identity has 2 attributes : a public key and a private key which are two different bytearray.
        /// The first
        /// the second is confidential and personal.
        /// </summary>
        private byte[] _publicKey;

        private byte[] _privateKey;

        /// <summary>
        /// Those 2 keys have both a getter.
        /// </summary>

        public byte[] PublicKey => this._publicKey;

        public byte[] PrivateKey => this._privateKey;

        /// <summary>
        /// Initializes a new identity
        /// </summary>
        /// <param name="publicKey">the public key of the new identity</param>
        /// <param name="privateKey">the private key of the new identity</param>
        public Identity(byte[] publicKey, byte[] privateKey)
        {
            this._publicKey = publicKey;
            this._privateKey = privateKey;
        }
    }
}

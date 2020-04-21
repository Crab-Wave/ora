using ORA.API.Encryption;
using System.Security.Cryptography;

namespace ORA.Core.Encryption
{
    public class RsaCipher : ICipher
    {
        private RSACryptoServiceProvider _rsa;
        private int _keySize;

        public int KeySize => this._keySize;

        public RsaCipher(int keySize, byte[] publicKey, byte[] privateKey)
        {
            this._keySize = keySize;
            this._rsa = new RSACryptoServiceProvider(keySize);
            this._rsa.ImportRSAPublicKey(publicKey, out int _);
            this._rsa.ImportRSAPrivateKey(privateKey, out int _);
        }

        public RsaCipher(int keySize)
        {
            this._keySize = keySize;
            this._rsa = new RSACryptoServiceProvider(keySize);
        }

        public byte[] GetPublicKey() => this._rsa.ExportRSAPublicKey();

        public byte[] GetPrivateKey() => this._rsa.ExportRSAPrivateKey();

        public byte[] Encrypt(byte[] message) => this._rsa.Encrypt(message, true);

        public byte[] Decrypt(byte[] encrypted) => this._rsa.Decrypt(encrypted, true);

        public void NewKey() => this._rsa = new RSACryptoServiceProvider(this._keySize);
    }
}

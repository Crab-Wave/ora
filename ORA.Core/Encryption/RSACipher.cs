using System.Security.Cryptography;
using ORA.API.Encryption;

namespace ORA.Core.Encryption
{
    public class RsaCipher : ICipher
    {
        private readonly int keysize;
        private RSAParameters privatekey;
        private RSAParameters publickey;

        public RsaCipher(int keysize)
        {
            this.keysize = keysize;
            using (var rsa = new RSACryptoServiceProvider(keysize))
            {
                rsa.PersistKeyInCsp = false;
                this.publickey = rsa.ExportParameters(false);
                this.privatekey = rsa.ExportParameters(true);
            }
        }

        public byte[] Encrypt(byte[] message)
        {
            byte[] encrypted;
            using (var rsa = new RSACryptoServiceProvider())
            {
                rsa.ImportParameters(this.publickey);
                encrypted = rsa.Encrypt(message, true);
            }

            return encrypted;
        }

        public byte[] Decrypt(byte[] encrypted)
        {
            byte[] decrypted;
            using (var rsa = new RSACryptoServiceProvider())
            {
                rsa.PersistKeyInCsp = false;
                rsa.ImportParameters(this.privatekey);
                decrypted = rsa.Decrypt(encrypted, true);
            }

            return decrypted;
        }

        public string GetPublicKey()
        {
            var rsa = new RSACryptoServiceProvider();
            rsa.ImportParameters(this.publickey);
            return rsa.ToXmlString(false);
        }

        public void NewKey()
        {
            using (var rsa = new RSACryptoServiceProvider(this.keysize))
            {
                rsa.PersistKeyInCsp = false;
                this.publickey = rsa.ExportParameters(false);
                this.privatekey = rsa.ExportParameters(true);
            }
        }
    }
}

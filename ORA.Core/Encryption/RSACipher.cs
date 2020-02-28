using ORA.API.Encryption;
using System.Security.Cryptography;

namespace ORA.Core.Encryption
{
    public class RsaCipher : ICipher
    {
        private RSAParameters publickey;
        private RSAParameters privatekey;
        private int keysize;

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

        public string GetPublicKey()
        {
            var rsa = new RSACryptoServiceProvider();
            rsa.ImportParameters(this.publickey);
            return rsa.ToXmlString(false);
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

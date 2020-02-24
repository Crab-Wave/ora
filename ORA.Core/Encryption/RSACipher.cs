using ORA.API.Encryption;
using System.Security.Cryptography;

namespace ORA.Core.Encryption
{
    public class RsaCipher : ICipher
    {
        private static RSAParameters keys;

        public RsaCipher()
        {
            var rsa = new RSACryptoServiceProvider(2048);
            keys = rsa.ExportParameters(true);
        }

        public string GetPublicKey()
        {

            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(2048);
            return rsa.ToXmlString(false);
        }

        public byte[] Encrypt(byte[] message)
        {
            byte[] encrypted;
            var rsa = new RSACryptoServiceProvider(2048);
            rsa.PersistKeyInCsp = false;
            rsa.ImportParameters(keys);
            encrypted = rsa.Encrypt(message, true);

            return encrypted;
        }

        public byte[] Decrypt(byte[] encrypted)
        {
            byte[] decrypted;
            var rsa = new RSACryptoServiceProvider(2048);
            rsa.PersistKeyInCsp = false;
            rsa.ImportParameters(keys);
            decrypted = rsa.Decrypt(encrypted, true);

            return decrypted;
        }

        public void NewKey()
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(2048);
            rsa.PersistKeyInCsp = false;
            rsa.Clear();

            var rsaa = new RSACryptoServiceProvider(2048);
            keys = rsaa.ExportParameters(true);
        }
    }
}

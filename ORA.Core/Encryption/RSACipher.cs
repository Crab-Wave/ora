using ORA.API.Encryption;
using System.Security.Cryptography;

namespace ORA.Core.Encryption
{
    public class RsaCipher : ICipher
    {
        private static RSAParameters publickey;
        private static RSAParameters privatekey;

        public RsaCipher()
        {
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.PersistKeyInCsp = false;
                publickey = rsa.ExportParameters(false);
                privatekey = rsa.ExportParameters(true);
            }
        }

        public string GetPublicKey()
        {
            var rsa = new RSACryptoServiceProvider(2048);
            rsa.ImportParameters(publickey);
            return rsa.ToXmlString(false);
        }

        public byte[] Encrypt(byte[] message)
        {
            byte[] encrypted;
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.PersistKeyInCsp = false;
                rsa.ImportParameters(publickey);
                encrypted = rsa.Encrypt(message, true);
            }

            return encrypted;
        }

        public byte[] Decrypt(byte[] encrypted)
        {
            byte[] decrypted;
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.PersistKeyInCsp = false;
                rsa.ImportParameters(privatekey);
                decrypted = rsa.Decrypt(encrypted, true);
            }
            return decrypted;
        }

        public void NewKey()
        {
            using var rsa = new RSACryptoServiceProvider(2048);
            {
                rsa.PersistKeyInCsp = false;
                rsa.Clear();
            }

            using (var rsaa = new RSACryptoServiceProvider(2048))
            {
                rsaa.PersistKeyInCsp = false;
                publickey = rsaa.ExportParameters(false);
                privatekey = rsaa.ExportParameters(true);
            }
        }
    }
}

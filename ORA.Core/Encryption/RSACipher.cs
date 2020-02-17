using ORA.API.Encryption;
using System.Security.Cryptography;

namespace ORA.Core.Encryption
{
    public class RsaCipher : ICipher
    {
        private static RSAParameters keys;
        private string container = "My Key Container";

        public RsaCipher()
        {
            CspParameters cspParameters = new CspParameters();
            cspParameters.KeyContainerName = this.container;
            using (var rsa = new RSACryptoServiceProvider(cspParameters))
            {
                keys = rsa.ExportParameters(true);
            }
        }

        public string GetPublicKey()
        {
            CspParameters cspParameters = new CspParameters();
            cspParameters.KeyContainerName = this.container;

            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(cspParameters);
            return rsa.ToXmlString(false);
        }

        public byte[] Encrypt(byte[] message)
        {
            CspParameters cspParameters = new CspParameters();
            cspParameters.KeyContainerName = this.container;
            byte[] encrypted;
            using (var rsa = new RSACryptoServiceProvider(cspParameters))
            {
                rsa.PersistKeyInCsp = false;
                rsa.ImportParameters(keys);
                encrypted = rsa.Encrypt(message, true);
            }
            return encrypted;
        }

        public byte[] Decrypt(byte[] encrypted)
        {
            CspParameters cspParameters = new CspParameters();
            cspParameters.KeyContainerName = this.container;
            byte[] decrypted;
            using (var rsa = new RSACryptoServiceProvider(cspParameters))
            {
                rsa.PersistKeyInCsp = false;
                rsa.ImportParameters(keys);
                decrypted = rsa.Decrypt(encrypted, true);
            }

            return decrypted;
        }

        public void NewKey()
        {
            CspParameters cspParameters = new CspParameters();
            cspParameters.KeyContainerName = this.container;
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(cspParameters);
            rsa.PersistKeyInCsp = false;
            rsa.Clear();

            using (var rsaa = new RSACryptoServiceProvider(cspParameters))
            {
                keys = rsaa.ExportParameters(true);
            }
        }
    }
}

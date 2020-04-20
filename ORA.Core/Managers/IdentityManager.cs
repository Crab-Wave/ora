using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ORA.API;
using ORA.API.Managers;
using ORA.Core.Encryption;

namespace ORA.Core.Managers
{
    // TODO
    public class IdentityManager : IIdentityManager
    {
        public const int KeysSize = 4096;

        private Identity _identity;

        public IdentityManager()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            path = Path.Combine(path, "identity.ora");
            if (File.Exists(path) && File.Exists(path = ".pub"))
            {
                try
                {
                    byte[] publicKey = File.ReadAllBytes(path + ".pub");
                    byte[] privateKey = File.ReadAllBytes(path);
                    this._identity = new Identity(publicKey, privateKey);
                }
                catch (Exception e)
                {
                    this._identity = this._generateIdentity(path);
                }
            }
            else
            {
                this._identity = this._generateIdentity(path);
            }
        }

        private Identity _generateIdentity(string path)
        {
            var rsaCipher = new RsaCipher(KeysSize);
            File.WriteAllBytes(path + ".pub", rsaCipher.GetPublicKey());
            File.WriteAllBytes(path, rsaCipher.GetPrivateKey());
            return new Identity(rsaCipher.GetPublicKey(), rsaCipher.GetPrivateKey());
        }

        public Identity GetIdentity() => this._identity;
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ORA.API;
using ORA.API.Managers;
using ORA.Core.Encryption;
using File = System.IO.File;

namespace ORA.Core.Managers
{
    // TODO
    public class IdentityManager : IIdentityManager
    {

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
                    byte[] publicKey = Convert.FromBase64String(File.ReadAllText(path + ".pub"));
                    byte[] privateKey = Convert.FromBase64String(File.ReadAllText(path));
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
            var rsaCipher = new RsaCipher();
            File.WriteAllText(path + ".pub", Convert.ToBase64String(rsaCipher.GetPublicKey()));
            File.WriteAllText(path, Convert.ToBase64String(rsaCipher.GetPrivateKey()));
            return new Identity(rsaCipher.GetPublicKey(), rsaCipher.GetPrivateKey());
        }

        public Identity GetIdentity() => this._identity;

        public Identity GenerateIdentity(string name) => this._generateIdentity(name);
    }
}

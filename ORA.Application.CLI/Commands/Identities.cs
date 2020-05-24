using System;
using CommandDotNet;
using CommandDotNet.Help;
using ORA.API;
using ORA.Application.CLI.Objects;

namespace ORA.Application.CLI.Commands
{
    [Command(Description = "Identities management command", Name = "identities")]
    public class Identities
    {
        [Command(Description = "Returns the public key of the identity", Name = "get", Usage = "get")]
        public void Get()
        {
            byte[] publicKey = Ora.GetIdentityManager().GetIdentity().PublicKey;
            Console.WriteLine($"Current identity public key: {Convert.ToBase64String(publicKey)}");
        }

        [Command(Description = "Generate a new identity", Name = "new", Usage = "new")]
        public void Generate()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            Ora.GetIdentityManager().GenerateIdentity(path);
            Console.WriteLine("New identity successfully generated");
        }
    }
}

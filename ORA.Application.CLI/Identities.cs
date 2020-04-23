using System;
using CommandDotNet;
using CommandDotNet.Help;
using ORA.API;
using ORA.Application.CLI.Objects;

namespace ORA.Application.CLI
{
    [Command(Description = "Identities management command", Name = "identities")]
    public class Identities
    {
        [Command(Description = "Get the public key ", Name = "get", Usage = "get")]
        public void Get()
        {
            byte[] publicKey = Ora.GetIdentityManager().GetIdentity().PublicKey;
            Console.WriteLine($"Your public key is {publicKey}");
        }

        [Command(Description = "Generate a new identity ", Name = "generate", Usage = "generate")]
        public void Generate()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            Ora.GetIdentityManager().GenerateIdentity(path);
            Console.WriteLine("An identity has been generate");
        }
    }
}

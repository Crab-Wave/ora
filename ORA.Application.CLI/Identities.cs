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
        [Command(Description = "Get the pubilc key ", Name = "get", Usage = "get")]
        public void Get([Operand(Description = "get identity")] IdentityPublicKeyModel publicKey)
        {
            byte[] publickey = Ora.GetIdentityManager().GetIdentity().PublicKey;
            Console.WriteLine($"Your public key is {publickey}");
        }
    }
}

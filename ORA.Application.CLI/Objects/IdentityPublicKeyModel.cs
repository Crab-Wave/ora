using CommandDotNet;
using FluentValidation.Attributes;
using ORA.Application.CLI.Validators;

namespace ORA.Application.CLI.Objects
{
    [Validator(typeof(IdentityPublicKeyValidator))]

    public class IdentityPublicKeyModel : IArgumentModel
    {
        public byte[] PublicKey
        {
            get;
            set;
        }
    }
}

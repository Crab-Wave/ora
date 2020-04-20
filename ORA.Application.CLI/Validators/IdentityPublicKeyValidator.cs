using System;
using FluentValidation;
using ORA.Application.CLI.Objects;
namespace ORA.Application.CLI.Validators
{
    public class IdentityPublicKeyValidator : AbstractValidator<IdentityPublicKeyModel>
    {
        public IdentityPublicKeyValidator()
        {
            this.RuleFor(model => model.PublicKey).Custom((s, context) =>
            {
                if (s.Length!=4096)
                {
                    context.AddFailure("Public key is not correctly generate");
                }
            });
        }
    }
}

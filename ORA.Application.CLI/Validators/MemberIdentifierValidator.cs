using System;
using FluentValidation;
using ORA.Application.CLI.Objects;

namespace ORA.Application.CLI.Validators
{
    public class MemberIdentifierValidator : AbstractValidator<ClusterIdentifierModel>
    {
        public MemberIdentifierValidator()
        {
            this.RuleFor(model => model.Identifier).Custom((s, context) =>
            {
                if (String.IsNullOrWhiteSpace(s))
                {
                    context.AddFailure("Identifier should not be empty");
                }
            });
        }
    }
}

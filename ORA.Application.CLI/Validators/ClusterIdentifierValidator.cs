using System;
using FluentValidation;
using ORA.Application.CLI.Objects;

namespace ORA.Application.CLI.Validators
{
    public class ClusterIdentifierValidator : AbstractValidator<ClusterIdentifierModel>
    {
        public ClusterIdentifierValidator()
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

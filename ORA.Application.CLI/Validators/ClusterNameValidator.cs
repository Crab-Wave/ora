using System;
using FluentValidation;
using ORA.Application.CLI.Objects;

namespace ORA.Application.CLI.Validators
{
    public class ClusterNameValidator : AbstractValidator<ClusterNameModel>
    {
        public ClusterNameValidator()
        {
            this.RuleFor(model => model.Name).Custom((s, context) =>
            {
                if (String.IsNullOrWhiteSpace(s))
                {
                    context.AddFailure("Name should not be empty");
                }
            });
        }
    }
}

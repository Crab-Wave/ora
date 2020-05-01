using System;
using FluentValidation;
using ORA.Application.CLI.Objects;

namespace ORA.Application.CLI.Validators
{
    public class UserNameValidator : AbstractValidator<UserNameModel>
    {
        public UserNameValidator()
        {
            this.RuleFor(model => model.UserName).Custom((s, context) =>
            {
                if (String.IsNullOrWhiteSpace(s))
                {
                    context.AddFailure("Name should not be empty");
                }
            });
        }
    }
}

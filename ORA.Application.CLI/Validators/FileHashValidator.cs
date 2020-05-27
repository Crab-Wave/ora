using System;
using FluentValidation;
using ORA.Application.CLI.Objects;

namespace ORA.Application.CLI.Validators
{
    public class FileHashValidator : AbstractValidator<FileHashModel>
    {
        public FileHashValidator()
        {
            this.RuleFor(model => model.Hash).Custom((s, context) =>
            {
                if (String.IsNullOrWhiteSpace(s))
                {
                    context.AddFailure("Hash should not be empty");
                }
            });
        }
    }
}

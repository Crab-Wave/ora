using CommandDotNet;
using FluentValidation.Attributes;
using ORA.Application.CLI.Validators;

namespace ORA.Application.CLI.Objects
{
    public class MemberIdentifierModel : IArgumentModel
    {
        public string Identifier
        {
            get;
            set;
        }
    }
}

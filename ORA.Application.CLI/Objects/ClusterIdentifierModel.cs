using CommandDotNet;
using FluentValidation.Attributes;
using ORA.Application.CLI.Validators;

namespace ORA.Application.CLI.Objects
{
    [Validator(typeof(ClusterIdentifierValidator))]
    public class ClusterIdentifierModel : IArgumentModel
    {
        public string Identifier
        {
            get;
            set;
        }
    }
}

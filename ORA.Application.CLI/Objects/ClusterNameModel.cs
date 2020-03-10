using CommandDotNet;
using FluentValidation.Attributes;
using ORA.Application.CLI.Validators;

namespace ORA.Application.CLI.Objects
{
    [Validator(typeof(ClusterNameValidator))]
    public class ClusterNameModel : IArgumentModel
    {
        public string Name
        {
            get;
            set;
        }
    }
}

using CommandDotNet;
using FluentValidation.Attributes;
using ORA.Application.CLI.Validators;

namespace ORA.Application.CLI.Objects
{
    [Validator(typeof(FileHashValidator))]
    public class FileHashModel : IArgumentModel
    {
        public string Hash
        {
            get;
            set;
        }
    }
}

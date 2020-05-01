using CommandDotNet;
using FluentValidation.Attributes;
using ORA.Application.CLI.Validators;

namespace ORA.Application.CLI.Objects
{
    [Validator(typeof(UserNameValidator))]
    public class UserNameModel : IArgumentModel
    {
        public string UserName
        {
            get;
            set;
        }
    }
}

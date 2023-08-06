using FluentValidation;
using System.Reflection.Metadata.Ecma335;
using UserService.Api.Queries;

namespace UserService.Api.Validations
{
    public class DeleteUserQueryValidator : AbstractValidator<DeleteUserQuery>
    {
        private readonly ILogger<DeleteUserQueryValidator> _logger;
        public DeleteUserQueryValidator(ILogger<DeleteUserQueryValidator> logger)
        {
            _logger = logger;
            _logger.LogInformation("HIIII");
            RuleFor(x => x.Id).Must(Validate).WithMessage(m => $"{m.Id} is not a valid Id");
        }
        private bool Validate(object id)
        {
            _logger.LogInformation("Hello");
            var type = id.GetType().Name.ToString();

            if (type == "String")
            {
                _logger.LogInformation("It is a string");
                return Guid.TryParse((string)id, out _);
            }
            else if (type == "Int32")
            {
                _logger.LogInformation("It is a int");
                return true;
            }
            return false;
        }
    }
}

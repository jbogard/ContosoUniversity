namespace ContosoUniversity.Infrastructure.Validation
{
    using System.Collections.Generic;
    using System.Linq;
    using FluentValidation;
    using FluentValidation.Results;

    public class FluentValidationMessageValidator<T> : IMessageValidator<T>
    {
        private readonly IEnumerable<IValidator<T>> _validators;

        public FluentValidationMessageValidator(IEnumerable<IValidator<T>> validators)
        {
            _validators = validators;
        }

        public IList<ValidationFailure> Validate(T message)
        {
            var context = new ValidationContext(message);

            var failures = _validators
                                .Select(v => v.Validate(context))
                                .SelectMany(result => result.Errors)
                                .Where(f => f != null)
                                .ToList();

            return failures;
        }
    }
}
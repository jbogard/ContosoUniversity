namespace ContosoUniversity.Infrastructure.Validation
{
    using System.Collections.Generic;
    using FluentValidation.Results;

    public interface IMessageValidator<in T>
    {
        IList<ValidationFailure> Validate(T message);
    }
}
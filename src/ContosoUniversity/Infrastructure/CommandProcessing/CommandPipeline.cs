namespace ContosoUniversity.Infrastructure.CommandProcessing
{
    using System.Linq;
    using FluentValidation;
    using MediatR;

    public class CommandPipeline<TRequest> : RequestHandler<TRequest> where TRequest : IRequest
    {
        private readonly RequestHandler<TRequest> _inner;
        private readonly IMessageValidator<TRequest> _validator;

        public CommandPipeline(RequestHandler<TRequest> inner, IMessageValidator<TRequest> validator)
        {
            _inner = inner;
            _validator = validator;
        }

        protected override void HandleCore(TRequest message)
        {
            var failures = _validator.Validate(message);

            if (failures.Any())
            {
                throw new ValidationException(failures);
            }

            _inner.Handle(message);
        }
    }
}
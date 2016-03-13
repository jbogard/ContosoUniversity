namespace ContosoUniversity.Features.Account
{
    using Infrastructure;
    using MediatR;
    using Microsoft.Owin.Security;

    public class LogOff
    {
        public class Command : IRequest
        {
            [PropertyModelBinder(typeof(UserNameModelBinder))]
            public string UserName { get; set; }
        }

        public class Handler : RequestHandler<Command>
        {
            private readonly IAuthenticationManager _manager;

            public Handler(IAuthenticationManager manager)
            {
                _manager = manager;
            }

            protected override void HandleCore(Command message)
            {
                _manager.SignOut();

                // Triggers and event: User {message.UserName} has logged off?
            }
        }
    }
}
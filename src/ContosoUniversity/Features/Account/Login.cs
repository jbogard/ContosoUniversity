namespace ContosoUniversity.Features.Account
{
    using MediatR;
    using Microsoft.AspNet.Identity.Owin;
    using Models;
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;

    public class Login
    {
        public class Command : IAsyncRequest
        {
            [Required]
            [Display(Name = "Email")]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }

        public class Handler : AsyncRequestHandler<Command>
        {
            private readonly SignInManager<User, int> _singInManager;

            public Handler(SignInManager<User, int> singInManager)
            {
                _singInManager = singInManager;
            }

            protected override async Task HandleCore(Command message)
            {
                var result = await _singInManager.PasswordSignInAsync(message.Email, message.Password, message.RememberMe, shouldLockout: false); 

                switch(result)
                {
                    case SignInStatus.Failure:
                    case SignInStatus.LockedOut:
                    case SignInStatus.RequiresVerification:
                        // Throws an exception: login exception?
                        break;
                    case SignInStatus.Success:
                        // Triggers an event: user logged in?
                        break;
                }
            }
        }
    }
}
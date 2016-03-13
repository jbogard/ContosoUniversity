namespace ContosoUniversity.Features.Account
{
    using MediatR;
    using Microsoft.AspNet.Identity;
    using Models;
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;

    public class Register
    {
        public class Command : IAsyncRequest
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public class Handler : AsyncRequestHandler<Command>
        {
            private readonly UserManager<User, int> _userManager;

            public Handler(UserManager<User, int> userManager)
            {
                _userManager = userManager;
            }

            protected override async Task HandleCore(Command message)
            {
                var user = new User(message.Email, message.Email)
                {
                    Email = message.Email,
                    UserName = message.Email
                };

                var result = await _userManager.CreateAsync(user, message.Password);

                if (!result.Succeeded)
                {
                    // Throws an exception: register exception?
                }

                // Triggers an event: Send confirmation email?
            }
        }
    }
}
namespace ContosoUniversity.Features.Account
{
    using Infrastructure;
    using MediatR;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    [Authorize]
    public class UiController : Controller
    {
        private readonly IMediator _mediator;

        public UiController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(Register.Command command)
        {
            // Why don't just use the: UserManager class?
            await _mediator.SendAsync(command);

            // Maybe redirect to an action: Please confirm your email.
            return this.RedirectToActionJson("Login");
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(Login.Command command)
        {
            // Why don't just use the: SignInManager class?
            await _mediator.SendAsync(command);

            return this.RedirectToActionJson("Dashboard");
        }

        public ActionResult Dashboard()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff(LogOff.Command command)
        {
            // Why don't just use the: IAuthenticationManager implementation class?
            _mediator.Send(command);

            return RedirectToAction("Index", "Home");
        }
    }
}
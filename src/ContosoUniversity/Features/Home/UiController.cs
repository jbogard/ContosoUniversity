namespace ContosoUniversity.Features.Home
{
    using MediatR;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    public class UiController : Controller
    {
        private readonly IMediator _mediator;

        public UiController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> About()
        {
            var data = await _mediator.SendAsync(new About.Query());

            return View(data);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
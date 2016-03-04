namespace ContosoUniversity.Features.Home
{
    using MediatR;
    using System.Linq;
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

        public ActionResult About()
        {
            var data = _mediator.Send(new About.Query());

            return View(data);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
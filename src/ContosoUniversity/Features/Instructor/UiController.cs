namespace ContosoUniversity.Features.Instructor
{
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using Infrastructure;
    using MediatR;

    public class UiController : Controller
    {
        private readonly IMediator _mediator;

        public UiController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<ActionResult> Index(Index.Query query)
        {
            var model = await _mediator.SendAsync(query);

            return View(model);
        }

        public async Task<ActionResult> Details(Details.Query query)
        {
            var model = await _mediator.SendAsync(query);

            return View(model);
        }

        public async Task<ActionResult> Create()
        {
            var model = await _mediator.SendAsync(new CreateEdit.Query());

            return View("CreateEdit", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateEdit.Command command)
        {
            await _mediator.SendAsync(command);
            
            return this.RedirectToActionJson("Index");
        }

        public async Task<ActionResult> Edit(CreateEdit.Query query)
        {
            var model = await _mediator.SendAsync(query);

            return View("CreateEdit", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(CreateEdit.Command command)
        {
            await _mediator.SendAsync(command);

            return this.RedirectToActionJson("Index");
        }

        public async Task<ActionResult> Delete(Delete.Query query)
        {
            var model = await _mediator.SendAsync(query);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Delete.Command command)
        {
            await _mediator.SendAsync(command);

            return this.RedirectToActionJson("Index");
        }
    }
}
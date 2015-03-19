namespace ContosoUniversity.Features.Department
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

        public async Task<ActionResult> Index()
        {
            var model = await _mediator.SendAsync(new IndexQuery());

            return View(model);
        }

        public async Task<ActionResult> Details(DetailsQuery query)
        {
            var department = await _mediator.SendAsync(query);

            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        public ActionResult Create()
        {
            return View(new CreateModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateModel model)
        {
            await _mediator.SendAsync(model);

            return this.RedirectToActionJson(c => c.Index());
        }

        public async Task<ActionResult> Edit(EditQuery query)
        {
            var department = await _mediator.SendAsync(query);

            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditModel model)
        {
            await _mediator.SendAsync(model);

            return this.RedirectToActionJson(c => c.Index());
        }

        public async Task<ActionResult> Delete(DeleteQuery query)
        {
            var model = await _mediator.SendAsync(query);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(DeleteModel model)
        {
            await _mediator.SendAsync(model);

            return this.RedirectToActionJson(c => c.Index());
        }
    }
}
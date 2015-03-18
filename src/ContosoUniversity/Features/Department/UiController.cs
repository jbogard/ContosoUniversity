namespace ContosoUniversity.Features.Department
{
    using System.Net;
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

        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var department = await _mediator.SendAsync(new DetailsQuery {Id = id.Value});

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

        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var department = await _mediator.SendAsync(new EditQuery {Id = id.Value});
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

        public async Task<ActionResult> Delete(int id)
        {
            var model = await _mediator.SendAsync(new DeleteQuery {Id = id});

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
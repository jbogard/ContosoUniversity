namespace ContosoUniversity.Features.Student
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using DAL;
    using Infrastructure;
    using MediatR;
    using Models;

    public class UiController : Controller
    {
        private readonly IMediator _mediator;
        private SchoolContext db = new SchoolContext();

        public UiController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: Student
        public ViewResult Index(Index.Query query)
        {
            var model = _mediator.Send(query);

            return View(model);
        }


        // GET: Student/Details/5
        public async Task<ActionResult> Details(Details.Query query)
        {
            var model = await _mediator.SendAsync(query);

            return View(model);
        }

        // GET: Student/Create
        public ActionResult Create()
        {
            return View(new Create.Command());
        }

        // POST: Student/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Create.Command command)
        {
            _mediator.Send(command);

            return this.RedirectToActionJson(c => c.Index(null));
        }


        // GET: Student/Edit/5
        public async Task<ActionResult> Edit(Edit.Query query)
        {
            var model = await _mediator.SendAsync(query);

            return View(model);
        }

        // POST: Student/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Edit.Command command)
        {
            await _mediator.SendAsync(command);

            return this.RedirectToActionJson(c => c.Index(null));
        }

        // GET: Student/Delete/5
        public ActionResult Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage =
                    "Delete failed. Try again, and if the problem persists see your system administrator.";
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Student/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                Student student = db.Students.Find(id);
                db.Students.Remove(student);
                db.SaveChanges();
            }
            catch (RetryLimitExceededException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                return RedirectToAction("Delete", new {id, saveChangesError = true});
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
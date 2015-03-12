namespace ContosoUniversity.Infrastructure
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Web.Mvc;
    using Newtonsoft.Json;

    public static class ControllerExtensions
    {
        public static ActionResult RedirectToActionJson<TController>(this Controller controller, Expression<Action<TController>> action)
    where TController : Controller
        {
            return controller.JsonNet(new
            {
                redirect = controller.Url.Action(action)
            }
                );
        }

        public static ActionResult RedirectToActionJson<TController>(this TController controller, Expression<Action<TController>> action)
            where TController : Controller
        {
            return controller.JsonNet(new
            {
                redirect = controller.Url.Action(action)
            }
                );
        }

        public static ActionResult RedirectToActionJson<TController>(this TController controller, string action)
            where TController : Controller
        {
            return controller.JsonNet(new
            {
                redirect = controller.Url.Action(action)
            }
                );
        }

        public static ContentResult JsonNet(this Controller controller, object model)
        {
            var serialized = JsonConvert.SerializeObject(model, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            return new ContentResult
            {
                Content = serialized,
                ContentType = "application/json"
            };
        }

        public static ContentResult RedirectToPreviousPage(this Controller controller, string anchor)
        {
            string redirectUrl = controller.Request.Params["previousPage"];

            if (redirectUrl != null)
            {
                redirectUrl = redirectUrl.Split(',').First();
            }
            redirectUrl += anchor;

            return controller.JsonNet(new { redirect = redirectUrl });
        }

        public static ContentResult RedirectToPreviousPage(this Controller controller)
        {
            return RedirectToPreviousPage(controller, "");
        }
    }
}
namespace ContosoUniversity.Infrastructure
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Web.Mvc;
    using System.Web.Routing;

    public static class LinkBuilder
    {
        public static string BuildUrlFromExpression<TController>(RequestContext requestContext, RouteCollection routes,
            Expression<Action<TController>> action) where TController : Controller
        {
            var url = Microsoft.Web.Mvc.LinkBuilder.BuildUrlFromExpression(requestContext, routes, action);

            return url.Replace("/Ui", typeof (TController).Namespace.Split('.').Last());
        }
    }
}
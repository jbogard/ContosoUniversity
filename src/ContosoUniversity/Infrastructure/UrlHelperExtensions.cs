namespace ContosoUniversity.Infrastructure
{
    using System;
    using System.Linq.Expressions;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Microsoft.Web.Mvc;

    public static class UrlHelperExtensions
    {
        public static string Action<TController>(this UrlHelper helper, Expression<Action<TController>> action)
    where TController : Controller
        {
            return LinkBuilder.BuildUrlFromExpression(helper.RequestContext, RouteTable.Routes, action);
        }

        public static string ActionWithReturnLink<TController>(Expression<Action<TController>> action, RequestContext requestContext)
          where TController : Controller
        {
            var url = LinkBuilder.BuildUrlFromExpression(requestContext, RouteTable.Routes, action);

            if (!url.Contains("?"))
                url += "?previousPage=" + requestContext.HttpContext.Request.Url.PathAndQuery;
            else
                url += "&previousPage=" + requestContext.HttpContext.Request.Url.PathAndQuery;

            return url;
        }
    }
}
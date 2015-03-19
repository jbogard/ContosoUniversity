namespace ContosoUniversity.Infrastructure
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Microsoft.Web.Mvc;

    public static class LinkBuilder
    {
        public static string BuildUrlFromExpression<TController>(RequestContext requestContext, RouteCollection routes,
            Expression<Action<TController>> action) where TController : Controller
        {
            var url = BuildUrlFromExpressionImpl(requestContext, routes, action);

            return url.Replace("/Ui", typeof (TController).Namespace.Split('.').Last());
        }

        private static string BuildUrlFromExpressionImpl<TController>(RequestContext context, RouteCollection routeCollection, Expression<Action<TController>> action) where TController : Controller
        {
            RouteValueDictionary routeValues = GetRouteValuesFromExpression(action);
            VirtualPathData vpd = routeCollection.GetVirtualPathForArea(context, routeValues);
            return (vpd == null) ? null : vpd.VirtualPath;
        }

        private static RouteValueDictionary GetRouteValuesFromExpression<TController>(Expression<Action<TController>> action) where TController : Controller
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            MethodCallExpression call = action.Body as MethodCallExpression;

            string controllerName = typeof(TController).Name;
            controllerName = controllerName.Substring(0, controllerName.Length - "Controller".Length);

            string actionName = GetTargetActionName(call.Method);

            var rvd = new RouteValueDictionary();
            rvd.Add("Controller", controllerName);
            rvd.Add("Action", actionName);

            ActionLinkAreaAttribute areaAttr = typeof(TController).GetCustomAttributes(typeof(ActionLinkAreaAttribute), true /* inherit */).FirstOrDefault() as ActionLinkAreaAttribute;
            if (areaAttr != null)
            {
                string areaName = areaAttr.Area;
                rvd.Add("Area", areaName);
            }

            AddParameterValuesFromExpressionToDictionary(rvd, call);
            return rvd;
        }

        private static string GetTargetActionName(MethodInfo methodInfo)
        {
            string methodName = methodInfo.Name;

            // targeting an async action?
            if (methodInfo.DeclaringType.IsSubclassOf(typeof(AsyncController)))
            {
                if (methodName.EndsWith("Async", StringComparison.OrdinalIgnoreCase))
                {
                    return methodName.Substring(0, methodName.Length - "Async".Length);
                }
            }

            // fallback
            return methodName;
        }

        private static void AddParameterValuesFromExpressionToDictionary(RouteValueDictionary rvd, MethodCallExpression call)
        {
            ParameterInfo[] parameters = call.Method.GetParameters();

            if (parameters.Length > 0)
            {
                for (int i = 0; i < parameters.Length; i++)
                {
                    Expression arg = call.Arguments[i];
                    object value = null;
                    ConstantExpression ce = arg as ConstantExpression;
                    if (ce != null)
                    {
                        // If argument is a constant expression, just get the value
                        value = ce.Value;
                    }
                    else
                    {
                        value = CachedExpressionCompiler.Evaluate(arg);
                    }
                    if (arg.Type.IsClass && arg.Type != typeof (string))
                    {
                        new RouteValueDictionary(value).ToList().ForEach(x => rvd[x.Key] = x.Value);
                    }
                    else
                    {
                        rvd.Add(parameters[i].Name, value);
                    }
                }
            }
        }
    }
}
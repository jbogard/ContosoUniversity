namespace ContosoUniversity.Infrastructure
{
    using System;
    using System.Web.Mvc;
    using System.Web.Routing;

    public class ControllerFactory : DefaultControllerFactory
    {
        protected override Type GetControllerType(RequestContext requestContext, string controllerName)
        {
            return
                typeof (ControllerFactory).Assembly.GetType("ContosoUniversity.Features." + controllerName +
                                                            ".UiController", false, true);
        }
    }
}
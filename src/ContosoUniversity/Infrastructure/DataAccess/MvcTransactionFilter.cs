namespace ContosoUniversity.Infrastructure.DataAccess
{
    using DAL;
    using System.Web.Mvc;

    public class MvcTransactionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Logger.Instance.Verbose("MvcTransactionFilter::OnActionExecuting");
            // var context = StructuremapMvc.ParentScope.CurrentNestedContainer.GetInstance<SchoolContext>();
            var context = DependencyResolver.Current.GetService<SchoolContext>();
            context.BeginTransaction();
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            // Logger.Instance.Verbose("MvcTransactionFilter::OnActionExecuted");
            // var instance = StructuremapMvc.ParentScope.CurrentNestedContainer.GetInstance<SchoolContext>();
            var instance = DependencyResolver.Current.GetService<SchoolContext>();
            instance.CloseTransaction(filterContext.Exception);
        }
    }
}
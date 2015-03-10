namespace ContosoUniversity.Infrastructure
{
    using System.Web.Mvc;
    using DAL;

    public class InstructorModelBinder : IModelBinder
    {
        private readonly SchoolContext _dbContext;

        public InstructorModelBinder(SchoolContext dbContext)
        {
            _dbContext = dbContext;
        }

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var attemptedValue = bindingContext.ValueProvider.GetValue(bindingContext.ModelName).AttemptedValue;

            return string.IsNullOrWhiteSpace(attemptedValue) ? null : _dbContext.Instructors.Find(int.Parse(attemptedValue));
        }
    }
}
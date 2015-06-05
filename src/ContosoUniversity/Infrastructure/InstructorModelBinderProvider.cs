namespace ContosoUniversity.Infrastructure
{
    using System;
    using System.Web.Mvc;
    using App_Start;
    using Models;

    public class InstructorModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(Type modelType)
        {
            return typeof(Instructor).IsAssignableFrom(modelType) ? StructuremapMvc.ParentScope.GetInstance<InstructorModelBinder>() : null;
        }
    }
}
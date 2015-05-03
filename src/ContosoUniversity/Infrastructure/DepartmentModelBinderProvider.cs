namespace ContosoUniversity.Infrastructure
{
    using System;
    using System.Web.Mvc;
    using App_Start;
    using Models;

    public class DepartmentModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(Type modelType)
        {
            return typeof(Department).IsAssignableFrom(modelType) ? StructuremapMvc.StructureMapDependencyScope.GetInstance<DepartmentModelBinder>() : null;
        }
    }
}
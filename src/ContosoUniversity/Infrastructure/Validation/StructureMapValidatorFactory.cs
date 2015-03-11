namespace ContosoUniversity.Infrastructure.Validation
{
    using System;
    using App_Start;
    using FluentValidation;

    public class StructureMapValidatorFactory : ValidatorFactoryBase
    {
        public override IValidator CreateInstance(Type validatorType)
        {
            return StructuremapMvc.StructureMapDependencyScope.CurrentNestedContainer.TryGetInstance(validatorType) as IValidator;
        }
    }
}
namespace ContosoUniversity.Infrastructure.Tags
{
    using Models;

    public class DepartmentSelectElementBuilder : EntitySelectElementBuilder<Department>
    {
        protected override int GetValue(Department instance)
        {
            return instance.DepartmentID;
        }

        protected override string GetDisplayValue(Department instance)
        {
            return instance.Name;
        }
    }
}
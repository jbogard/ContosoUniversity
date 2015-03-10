namespace ContosoUniversity.Infrastructure.Tags
{
    using Models;

    public class InstructorSelectElementBuilder : EntitySelectElementBuilder<Instructor>
    {
        protected override int GetValue(Instructor instance)
        {
            return instance.ID;
        }

        protected override string GetDisplayValue(Instructor instance)
        {
            return instance.FullName;
        }
    }
}
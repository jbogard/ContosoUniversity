namespace ContosoUniversity.Features.Department
{
    using FluentValidation;

    public class EditValidator : AbstractValidator<EditModel>
    {
        public EditValidator()
        {
            RuleFor(m => m.Name).NotNull().Length(3, 50);
            RuleFor(m => m.Budget).NotNull();
            RuleFor(m => m.StartDate).NotNull();
            RuleFor(m => m.Administrator).NotNull();
        }
    }
}
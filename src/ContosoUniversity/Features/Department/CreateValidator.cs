namespace ContosoUniversity.Features.Department
{
    using FluentValidation;

    public class CreateValidator : AbstractValidator<CreateModel>
    {
        public CreateValidator()
        {
            RuleFor(m => m.Name).NotNull().Length(3, 50);
            RuleFor(m => m.Budget).NotNull();
            RuleFor(m => m.StartDate).NotNull();
            RuleFor(m => m.Instructor).NotNull();
        }
    }
}
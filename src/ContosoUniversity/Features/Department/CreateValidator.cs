namespace ContosoUniversity.Features.Department
{
    using FluentValidation;

    public class CreateValidator : AbstractValidator<CreateModel>
    {
        public CreateValidator()
        {
            RuleFor(m => m.Name).NotNull().Length(3, 50);
        }
    }
}
namespace ContosoUniversity.Features.Department
{
    using System.Threading.Tasks;
    using AutoMapper;
    using DAL;
    using MediatR;
    using Models;

    public class CreateHandler : AsyncRequestHandler<CreateModel>
    {
        private readonly SchoolContext _context;

        public CreateHandler(SchoolContext context)
        {
            _context = context;
        }

        protected override async Task HandleCore(CreateModel message)
        {
            var department = Mapper.Map<CreateModel, Department>(message);

            _context.Departments.Add(department);
        }
    }
}
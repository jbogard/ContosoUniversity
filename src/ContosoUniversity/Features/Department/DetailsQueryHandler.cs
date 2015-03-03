namespace ContosoUniversity.Features.Department
{
    using System.Threading.Tasks;
    using DAL;
    using MediatR;
    using Models;

    public class DetailsQueryHandler : IAsyncRequestHandler<DetailsQuery, Department>
    {
        private readonly SchoolContext _context;

        public DetailsQueryHandler(SchoolContext context)
        {
            _context = context;
        }

        public async Task<Department> Handle(DetailsQuery message)
        {
            string query = "SELECT * FROM Department WHERE DepartmentID = @p0";
            Department department = await _context.Departments.SqlQuery(query, message.Id).SingleOrDefaultAsync();

            return department;
        }
    }
}
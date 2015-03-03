namespace ContosoUniversity.Features.Department
{
    using System.Threading.Tasks;
    using DAL;
    using MediatR;
    using Models;

    public class DetailsQueryHandler : IAsyncRequestHandler<DetailsQuery, DetailsModel>
    {
        private readonly SchoolContext _context;

        public DetailsQueryHandler(SchoolContext context)
        {
            _context = context;
        }

        public async Task<DetailsModel> Handle(DetailsQuery message)
        {
            string query = @"
SELECT d.*, p.LastName + ', ' + p.FirstName AS [AdministratorFullName]
FROM Department d
LEFT OUTER JOIN Person p ON d.InstructorID = p.ID
WHERE d.DepartmentID = @p0";
            DetailsModel department = await _context.Database.SqlQuery<DetailsModel>(query, message.Id).SingleOrDefaultAsync();

            return department;
        }
    }
}
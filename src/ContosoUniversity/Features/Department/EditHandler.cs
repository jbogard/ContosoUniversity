namespace ContosoUniversity.Features.Department
{
    using System.Threading.Tasks;
    using AutoMapper;
    using DAL;
    using MediatR;

    public class EditHandler : AsyncRequestHandler<EditModel>
    {
        private readonly SchoolContext _db;

        public EditHandler(SchoolContext db)
        {
            _db = db;
        }

        protected override async Task HandleCore(EditModel message)
        {
            var dept = await _db.Departments.FindAsync(message.DepartmentID);

            Mapper.Map(message, dept);
        }
    }
}
namespace ContosoUniversity.Features.Department
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;
    using DAL;
    using Infrastructure.Mapping;
    using MediatR;

    public class DeleteQuery : IAsyncRequest<DeleteModel>
    {
        public int Id { get; set; }
    }

    public class DeleteModel : IAsyncRequest
    {
        public string Name { get; set; }

        public decimal Budget { get; set; }

        public DateTime StartDate { get; set; }

        public int DepartmentID { get; set; }

        [Display(Name = "Administrator")]
        public string AdministratorFullName { get; set; }
        public byte[] RowVersion { get; set; }
    }

    public class DeleteQueryHandler : IAsyncRequestHandler<DeleteQuery, DeleteModel>
    {
        private readonly SchoolContext _db;

        public DeleteQueryHandler(SchoolContext db)
        {
            _db = db;
        }

        public async Task<DeleteModel> Handle(DeleteQuery message)
        {
            var department = await _db.Departments
                .Where(d => d.DepartmentID == message.Id)
                .ProjectToSingleOrDefaultAsync<DeleteModel>();

            return department;
        }
    }

    public class DeleteHandler : AsyncRequestHandler<DeleteModel>
    {
        private readonly SchoolContext _db;

        public DeleteHandler(SchoolContext db)
        {
            _db = db;
        }

        protected override async Task HandleCore(DeleteModel message)
        {
            var department = await _db.Departments.FindAsync(message.DepartmentID);

            _db.Departments.Remove(department);
        }
    }
}
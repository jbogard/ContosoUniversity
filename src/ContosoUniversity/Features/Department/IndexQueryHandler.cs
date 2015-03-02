namespace ContosoUniversity.Features.Department
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Threading.Tasks;
    using DAL;
    using MediatR;
    using Models;

    public class IndexQueryHandler : IAsyncRequestHandler<IndexQuery, List<Department>>
    {
        private readonly SchoolContext _context;

        public IndexQueryHandler(SchoolContext context)
        {
            _context = context;
        }

        public Task<List<Department>> Handle(IndexQuery message)
        {
            return _context.Departments.Include(d => d.Administrator).ToListAsync();
        }
    }
}
namespace ContosoUniversity.Features.Department
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper.QueryableExtensions;
    using DAL;
    using Infrastructure.Mapping;
    using MediatR;

    public class IndexQueryHandler : IAsyncRequestHandler<IndexQuery, List<IndexModel>>
    {
        private readonly SchoolContext _context;

        public IndexQueryHandler(SchoolContext context)
        {
            _context = context;
        }

        public async Task<List<IndexModel>> Handle(IndexQuery message)
        {
            return await _context.Departments
                .Project().ToListAsync<IndexModel>();
        }
    }
}
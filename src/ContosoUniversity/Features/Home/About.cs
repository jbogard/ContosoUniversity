namespace ContosoUniversity.Features.Home
{
    using ContosoUniversity.DAL;
    using MediatR;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;

    public class About
    {
        public class EnrollmentDateGroup
        {
            [DataType(DataType.Date)]
            public DateTime? EnrollmentDate { get; set; }

            public int StudentCount { get; set; }
        }

        public class Query : IAsyncRequest<IEnumerable<EnrollmentDateGroup>> { }

        public class Handler : IAsyncRequestHandler<Query, IEnumerable<EnrollmentDateGroup>>
        {
            private readonly SchoolContext _dbContext;

            public Handler(SchoolContext dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task<IEnumerable<EnrollmentDateGroup>> Handle(Query message)
            {
                // Commenting out LINQ to show how to do the same thing in SQL.
                //IQueryable<EnrollmentDateGroup> = from student in db.Students
                //           group student by student.EnrollmentDate into dateGroup
                //           select new EnrollmentDateGroup()
                //           {
                //               EnrollmentDate = dateGroup.Key,
                //               StudentCount = dateGroup.Count()
                //           };

                // SQL version of the above LINQ code.
                string query = "SELECT EnrollmentDate, COUNT(*) AS StudentCount "
                    + "FROM Person "
                    + "WHERE Discriminator = 'Student' "
                    + "GROUP BY EnrollmentDate";
                IEnumerable<EnrollmentDateGroup> data = await _dbContext.Database.SqlQuery<EnrollmentDateGroup>(query).ToListAsync();

                return data;
            }
        }
    }
}
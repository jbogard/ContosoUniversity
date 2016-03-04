namespace ContosoUniversity.Features.Home
{
    using ContosoUniversity.DAL;
    using ContosoUniversity.ViewModels;
    using MediatR;
    using System.Collections.Generic;
    using System.Linq;

    public class About
    {
        public class Query : IRequest<IEnumerable<EnrollmentDateGroup>> { }

        public class Handler : IRequestHandler<Query, IEnumerable<EnrollmentDateGroup>>
        {
            private readonly SchoolContext _dbContext;

            public Handler(SchoolContext dbContext)
            {
                _dbContext = dbContext;
            }

            public IEnumerable<EnrollmentDateGroup> Handle(Query message)
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
                IEnumerable<EnrollmentDateGroup> data = _dbContext.Database.SqlQuery<EnrollmentDateGroup>(query);

                return data.ToList();
            }
        }
    }
}
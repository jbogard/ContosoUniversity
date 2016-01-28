namespace ContosoUniversity.Features.Student
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using DAL;
    using MediatR;

    public class Delete
    {
        public class Query : IAsyncRequest<Command>
        {
            public int Id { get; set; }
        }

        public class Command : IAsyncRequest
        {
            public int ID { get; set; }
            [Display(Name = "First Name")]
            public string FirstMidName { get; set; }
            public string LastName { get; set; }
            public DateTime EnrollmentDate { get; set; }
        }

        public class QueryHandler : IAsyncRequestHandler<Query, Command>
        {
            private readonly SchoolContext _db;
            private readonly MapperConfiguration _config;

            public QueryHandler(SchoolContext db, MapperConfiguration config)
            {
                _db = db;
                _config = config;
            }

            public async Task<Command> Handle(Query message)
            {
                return await _db.Students.Where(s => s.ID == message.Id).ProjectToSingleOrDefaultAsync<Command>(_config);
            }
        }

        public class CommandHandler : AsyncRequestHandler<Command>
        {
            private readonly SchoolContext _db;

            public CommandHandler(SchoolContext db)
            {
                _db = db;
            }

            protected override async Task HandleCore(Command message)
            {
                var student = await _db.Students.FindAsync(message.ID);

                _db.Students.Remove(student);
            }
        }

    }
}
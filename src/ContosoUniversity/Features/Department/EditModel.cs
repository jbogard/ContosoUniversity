namespace ContosoUniversity.Features.Department
{
    using System;
    using MediatR;
    using Models;

    public class EditModel : IAsyncRequest
    {
        public string Name { get; set; }

        public decimal? Budget { get; set; }

        public DateTime? StartDate { get; set; }

        public Instructor Administrator { get; set; }
        public int DepartmentID { get; set; }
        public byte[] RowVersion { get; set; }
    }
}
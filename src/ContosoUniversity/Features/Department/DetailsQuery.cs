namespace ContosoUniversity.Features.Department
{
    using MediatR;
    using Models;

    public class DetailsQuery : IAsyncRequest<Department>
    {
        public int? Id { get; set; }
    }
}
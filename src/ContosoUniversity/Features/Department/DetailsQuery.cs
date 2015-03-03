namespace ContosoUniversity.Features.Department
{
    using MediatR;
    using Models;

    public class DetailsQuery : IAsyncRequest<DetailsModel>
    {
        public int? Id { get; set; }
    }
}
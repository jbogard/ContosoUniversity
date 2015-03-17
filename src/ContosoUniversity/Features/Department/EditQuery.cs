namespace ContosoUniversity.Features.Department
{
    using MediatR;

    public class EditQuery : IAsyncRequest<EditModel>
    {
        public int Id { get; set; }
    }
}
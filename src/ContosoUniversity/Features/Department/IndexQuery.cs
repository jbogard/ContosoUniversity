namespace ContosoUniversity.Features.Department
{
    using System.Collections.Generic;
    using MediatR;

    public class IndexQuery : IAsyncRequest<List<IndexModel>>
    {
    }
}
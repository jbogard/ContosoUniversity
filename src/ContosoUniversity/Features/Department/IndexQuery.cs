namespace ContosoUniversity.Features.Department
{
    using System.Collections.Generic;
    using MediatR;
    using Models;

    public class IndexQuery : IAsyncRequest<List<Department>>
    {
         
    }
}
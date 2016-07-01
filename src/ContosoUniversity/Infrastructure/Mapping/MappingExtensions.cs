namespace ContosoUniversity.Infrastructure.Mapping
{
    using System.Linq;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using DelegateDecompiler;
    using PagedList;

    public static class MapperExtensions
    {
        public static IPagedList<TDestination> ProjectToPagedList<TDestination>(this IQueryable queryable, int pageNumber, int pageSize)
        {
            return queryable.ProjectTo<TDestination>().Decompile().ToPagedList(pageNumber, pageSize);
        }
    }
}
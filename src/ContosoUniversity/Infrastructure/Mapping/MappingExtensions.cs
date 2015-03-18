namespace ContosoUniversity.Infrastructure.Mapping
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper.QueryableExtensions;
    using DelegateDecompiler;
    using DelegateDecompiler.EntityFramework;

    public static class MapperExtensions
    {
        public static async Task<List<TDestination>> ProjectToListAsync<TDestination>(this IQueryable queryable)
        {
            return await queryable.ProjectTo<TDestination>().DecompileAsync().ToListAsync();
        }

        public static IQueryable<TDestination> ProjectToQueryable<TDestination>(this IQueryable queryable)
        {
            return queryable.ProjectTo<TDestination>().Decompile();
        }

        public static async Task<TDestination> ProjectToSingleOrDefaultAsync<TDestination>(this IQueryable queryable)
        {
            return await queryable.ProjectTo<TDestination>().DecompileAsync().SingleOrDefaultAsync();
        }
    }
}
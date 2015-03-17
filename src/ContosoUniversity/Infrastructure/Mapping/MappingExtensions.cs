namespace ContosoUniversity.Infrastructure.Mapping
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using DelegateDecompiler;
    using DelegateDecompiler.EntityFramework;
    using StructureMap;

    public static class MapperExtensions
    {
        public static IMapExpression<TSource> Map<TSource>(this TSource source)
        {
            return new MapExpression<TSource>(source);
        }

        public static void Fill<TSource, TDestination>(this TDestination destination, TSource source, IContainer container)
        {
            Mapper.Map(source, destination, opts => opts.ConstructServicesUsing(container.GetInstance));
        }

        public static IEnumerableMapExpression<IEnumerable<TSource>> MapAll<TSource>(this IEnumerable<TSource> source)
        {
            return new CollectionMapExpression<IEnumerable<TSource>>(source);
        }

        public static List<TDestination> ToList<TDestination>(this IProjectionExpression projectionExpression)
        {
            return projectionExpression.To<TDestination>().Decompile().ToList();
        }

        public static async Task<List<TDestination>> ToListAsync<TDestination>(this IProjectionExpression projectionExpression)
        {
            return await projectionExpression.To<TDestination>().DecompileAsync().ToListAsync();
        }

        public static IQueryable<TDestination> ToQueryable<TDestination>(this IProjectionExpression projectionExpression)
        {
            return projectionExpression.To<TDestination>().Decompile();
        }

        public static TDestination[] ToArray<TDestination>(this IProjectionExpression projectionExpression)
        {
            return projectionExpression.To<TDestination>().Decompile().ToArray();
        }

        public static TDestination ToSingleOrDefault<TDestination>(this IProjectionExpression projectionExpression)
        {
            return projectionExpression.To<TDestination>().Decompile().ToList().SingleOrDefault();
        }

        public static async Task<TDestination> ToSingleOrDefaultAsync<TDestination>(this IProjectionExpression projectionExpression)
        {
            return await projectionExpression.To<TDestination>().DecompileAsync().SingleOrDefaultAsync();
        }

        private class MapExpression<TSource> : IMapExpression<TSource>
        {
            private readonly TSource _source;

            public MapExpression(TSource source)
            {
                _source = source;
            }

            public TDestination To<TDestination>(IContainer container)
            {
                return Mapper.Map<TSource, TDestination>(_source, o => o.ConstructServicesUsing(container.GetInstance));
            }

            public TDestination To<TDestination>()
            {
                return Mapper.Map<TSource, TDestination>(_source);
            }
        }

        private class CollectionMapExpression<TSource> : IEnumerableMapExpression<TSource>
        {
            private readonly TSource _source;

            public CollectionMapExpression(TSource source)
            {
                _source = source;
            }

            public IList<TDestination> To<TDestination>(IContainer container)
            {
                return Mapper.Map<TSource, IList<TDestination>>(_source, o => o.ConstructServicesUsing(container.GetInstance));
            }

            public IList<TDestination> To<TDestination>()
            {
                return Mapper.Map<TSource, IList<TDestination>>(_source);
            }
        }


        public interface IMapExpression<TSource>
        {
            TDestination To<TDestination>(IContainer container);
            TDestination To<TDestination>();
        }

        public interface IEnumerableMapExpression<TSource>
        {
            IList<TDestination> To<TDestination>(IContainer container);
            IList<TDestination> To<TDestination>();
        }
    }
}
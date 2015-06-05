namespace ContosoUniversity.IntegrationTests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Fixie.Conventions;
    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.Kernel;

    public static class FixieExtensions
    {
        public static IEnumerable<object[]> ResolveParametersWith(this MethodInfo methodInfo, IFixture fixture)
        {
            return Enumerable.Repeat(
                methodInfo
                .GetParameters()
                .Select(parameterInfo => new SpecimenContext(fixture).Resolve(parameterInfo))
                .ToArray(),
                1);
        }

        public static ClassExpression IsInNamespaceOf<T>(this ClassExpression expr)
        {
            return expr.InTheSameNamespaceAs(typeof(T));
        }

        public static ClassExpression AreInIntegrationTestNamespace(this ClassExpression expr)
        {
            return expr.Where(type => type.Namespace.Contains("IntegrationTests"));
        }

        public static ClassExpression ConstructorHasArguments(this ClassExpression filter)
        {
            return filter.Where(t => t.GetConstructors(BindingFlags.Public | BindingFlags.Instance)
                    .Any(x => x.GetParameters().Any()));
        }

        public static ClassExpression ConstructorDoesntHaveArguments(this ClassExpression filter)
        {
            return filter.Where(t => t.GetConstructors(BindingFlags.Public | BindingFlags.Instance)
                    .All(x => x.GetParameters().Length == 0));
        }
    }
}
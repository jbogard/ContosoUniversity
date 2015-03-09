namespace ContosoUniversity.Infrastructure.Tags
{
    using System;
    using HtmlTags;
    using HtmlTags.Conventions;
    using HtmlTags.UI;
    using HtmlTags.UI.Elements;
    using StructureMap;
    using StructureMap.Configuration.DSL;

    public class HtmlTagRegistry : Registry
    {

        public HtmlTagRegistry()
        {

            var htmlConventionLibrary = new HtmlConventionLibrary();
            new DefaultAspNetMvcHtmlConventions().Apply(htmlConventionLibrary);
            new DefaultHtmlConventions().Apply(htmlConventionLibrary);
            For<HtmlConventionLibrary>().Use(htmlConventionLibrary);

            For<ITagRequestActivator>().AddInstances(c =>
            {
                c.Type<ElementRequestActivator>();
                c.Type<ServiceLocatorTagRequestActivator>();
            });
            For<IServiceLocator>().Use<StructureMapServiceLocator>();
            For(typeof(IElementGenerator<>)).Use(typeof(ElementGenerator<>));
            For<IElementNamingConvention>().Use<DefaultElementNamingConvention>();

            Scan(s =>
            {
                s.AssemblyContainingType<HtmlTag>();
                s.WithDefaultConventions();
            });

        }

        public class StructureMapServiceLocator : IServiceLocator
        {
            private readonly IContainer _container;

            public StructureMapServiceLocator(IContainer container)
            {
                _container = container;
            }

            public object GetInstance(Type type)
            {
                return _container.GetInstance(type);
            }

            public IContainer Container { get { return _container; } }


            public TService GetInstance<TService>()
            {
                return _container.GetInstance<TService>();
            }

            public TService GetInstance<TService>(string name)
            {
                return _container.GetInstance<TService>(name);
            }
        }

    }
}
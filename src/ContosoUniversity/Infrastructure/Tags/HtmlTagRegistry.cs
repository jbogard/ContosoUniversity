using StructureMap;

namespace ContosoUniversity.Infrastructure.Tags
{
    using HtmlTags.Conventions;

    public class HtmlTagRegistry : Registry
    {

        public HtmlTagRegistry()
        {

            var htmlConventionLibrary = new HtmlConventionLibrary();
            new DefaultAspNetMvcHtmlConventions().Apply(htmlConventionLibrary);
            new DefaultHtmlConventions().Apply(htmlConventionLibrary);
            For<HtmlConventionLibrary>().Use(htmlConventionLibrary);
        }
    }
}
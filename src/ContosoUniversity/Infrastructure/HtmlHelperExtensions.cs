namespace ContosoUniversity.Infrastructure
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Web.Mvc;
    using App_Start;
    using HtmlTags;
    using HtmlTags.Conventions;
    using HtmlTags.Reflection;
    using HtmlTags.UI.Elements;

    public static class HtmlHelperExtensions
    {
        public static HtmlTag Input<T>(this HtmlHelper<T> helper, Expression<Func<T, object>> expression)
            where T : class
        {
            var generator = GetGenerator<T>();
            return generator.InputFor(expression, model: helper.ViewData.Model);
        }

        public static HtmlTag Label<T>(this HtmlHelper<T> helper, Expression<Func<T, object>> expression)
            where T : class
        {
            var generator = GetGenerator<T>();
            return generator.LabelFor(expression, model: helper.ViewData.Model);
        }

        public static HtmlTag Validator<T>(this HtmlHelper<T> helper,
Expression<Func<T, object>> expression) where T : class
        {
            var tagGeneratorFactory = StructuremapMvc.StructureMapDependencyScope.CurrentNestedContainer.GetInstance<ITagGeneratorFactory>();
            var tagGenerator = tagGeneratorFactory.GeneratorFor<ElementRequest>();
            var request = new ElementRequest(expression.ToAccessor())
            {
                Model = helper.ViewData.Model
            };

            var tag = tagGenerator.Build(request, "Validators");

            return tag;
        } 
        private static IElementGenerator<T> GetGenerator<T>() where T : class
        {
            return StructuremapMvc.StructureMapDependencyScope.CurrentNestedContainer.GetInstance<IElementGenerator<T>>();
        }
    }
}
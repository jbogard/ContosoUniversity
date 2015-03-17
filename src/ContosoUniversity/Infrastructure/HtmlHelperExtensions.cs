namespace ContosoUniversity.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Web.Mvc;
    using System.Web.Mvc.Html;
    using System.Web.Routing;
    using App_Start;
    using HtmlTags;
    using HtmlTags.Conventions;
    using HtmlTags.Reflection;
    using HtmlTags.UI.Elements;
    using Microsoft.Web.Mvc;

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

        public static HtmlTag Display<T>(this HtmlHelper<T> helper, Expression<Func<T, object>> expression)
            where T : class
        {
            var generator = GetGenerator<T>();
            return generator.DisplayFor(expression, model: helper.ViewData.Model);
        }

        public static HtmlTag DisplayLabel<T>(this HtmlHelper<T> helper, Expression<Func<T, object>> expression)
            where T : class
        {
            return DisplayLabelImpl(expression);
        }

        public static HtmlTag DisplayLabel<T>(this HtmlHelper<IList<T>> helper, Expression<Func<T, object>> expression)
            where T : class
        {
            return DisplayLabelImpl(expression);
        }

        private static HtmlTag DisplayLabelImpl<T>(Expression<Func<T, object>> expression) where T : class
        {
            var tagGeneratorFactory =
                StructuremapMvc.StructureMapDependencyScope.CurrentNestedContainer.GetInstance<ITagGeneratorFactory>();
            var tagGenerator = tagGeneratorFactory.GeneratorFor<ElementRequest>();
            var request = new ElementRequest(expression.ToAccessor())
            {
                Model = default(T)
            };

            var tag = tagGenerator.Build(request, "DisplayLabels");

            return tag;
        }

        public static MvcHtmlString DisplayNameFor<TModel, TValue>(this HtmlHelper<IList<TModel>> html,
            Expression<Func<TModel, TValue>> expression)
        {
            return new HtmlHelper<IEnumerable<TModel>>(html.ViewContext, html.ViewDataContainer).DisplayNameFor(expression);
        }

        public static HtmlTag InputBlock<T>(this HtmlHelper<T> helper,
            Expression<Func<T, object>> expression,
            Action<HtmlTag> inputModifier = null) where T : class
        {
            inputModifier = inputModifier ?? (_ => { });

            var divTag = new HtmlTag("div");
            divTag.AddClass("col-md-10");

            var inputTag = helper.Input(expression);
            inputModifier(inputTag);

            divTag.Append(inputTag);

            return divTag;
        }

        public static HtmlTag FormBlock<T>(this HtmlHelper<T> helper,
            Expression<Func<T, object>> expression,
            Action<HtmlTag> labelModifier = null,
            Action<HtmlTag> inputBlockModifier = null,
            Action<HtmlTag> inputModifier = null
            ) where T : class
        {
            labelModifier = labelModifier ?? (_ => { });
            inputBlockModifier = inputBlockModifier ?? (_ => { });

            var divTag = new HtmlTag("div");
            divTag.AddClass("form-group");

            var labelTag = helper.Label(expression);
            labelModifier(labelTag);

            var inputBlockTag = helper.InputBlock(
                expression,
                inputModifier);
            inputBlockModifier(inputBlockTag);

            divTag.Append(labelTag);
            divTag.Append(inputBlockTag);

            return divTag;
        }

        public static HtmlTag ValidationDiv(this HtmlHelper helper)
        {
            return new HtmlTag("div")
                .Id("validationSummary")
                .AddClass("alert")
                .AddClass("alert-danger")
                .AddClass("hidden");
        }

        public static HtmlTag Link<TController>(this HtmlHelper helper, Expression<Action<TController>> action, string linkText) where TController : Controller
        {
            var url = LinkBuilder.BuildUrlFromExpression(helper.ViewContext.RequestContext, RouteTable.Routes,
                action);

            return Link(helper, linkText, url);
        }

        private static HtmlTag Link(HtmlHelper helper, string linkText, string url)
        {
            url = "~/" + url;
            url = UrlHelper.GenerateContentUrl(url, helper.ViewContext.HttpContext);

            return new HtmlTag("a", t =>
            {
                t.Text(linkText);
                t.Attr("href", url);
            });
        }

        private static IElementGenerator<T> GetGenerator<T>() where T : class
        {
            return
                StructuremapMvc.StructureMapDependencyScope.CurrentNestedContainer.GetInstance<IElementGenerator<T>>();
        }
    }
}
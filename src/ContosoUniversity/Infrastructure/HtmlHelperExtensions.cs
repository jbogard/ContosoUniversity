namespace ContosoUniversity.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Mvc.Html;
    using System.Web.Routing;
    using App_Start;
    using FluentValidation.Internal;
    using Glimpse.AspNet.Tab;
    using HtmlTags;
    using HtmlTags.Conventions;
    using HtmlTags.Conventions.Elements;

    public static class HtmlHelperExtensions
    {
        public static string RequireJs(this HtmlHelper helper)
        {
            var values = helper.ViewContext.RouteData.Values;
            var controllerName = values["controller"].ToString();
            var viewContext = (RazorView)helper.ViewContext.View;
            var viewName = Path.GetFileNameWithoutExtension(viewContext.ViewPath);
            var requirePath = VirtualPathUtility.ToAbsolute("~/Content/js/lib/require-2.1.18.js");

            var loc = string.Format("~/Features/{0}/{1}.js", controllerName, viewName);

            if (File.Exists(helper.ViewContext.HttpContext.Server.MapPath(loc)))
            {
                var scriptBlock = "<script data-main='{0}' src='{1}'></script>";

                return string.Format(scriptBlock, VirtualPathUtility.ToAbsolute(loc), requirePath);
            }

            return string.Empty;
        }

        public static HtmlTag Input<T>(this HtmlHelper<T> helper, Expression<Func<T, object>> expression)
            where T : class
        {
            var generator = GetGenerator(helper.ViewData.Model);
            return generator.InputFor(expression);
        }

        public static HtmlTag Label<T>(this HtmlHelper<T> helper, Expression<Func<T, object>> expression)
            where T : class
        {
            var generator = GetGenerator(helper.ViewData.Model);
            return generator.LabelFor(expression);
        }

        public static HtmlTag Display<T>(this HtmlHelper<T> helper, Expression<Func<T, object>> expression)
            where T : class
        {
            var generator = GetGenerator(helper.ViewData.Model);
            return generator.DisplayFor(expression);
        }

        public static HtmlTag DisplayLabel<T>(this HtmlHelper<T> helper, Expression<Func<T, object>> expression)
            where T : class
        {
            var generator = GetGenerator(helper.ViewData.Model);
            return generator.TagFor(expression, "DisplayLabels");
        }

        public static HtmlTag DisplayLabel<T>(this HtmlHelper<IList<T>> helper, Expression<Func<T, object>> expression)
            where T : class
        {
            var generator = GetGenerator(default(T));
            return generator.TagFor(expression, "DisplayLabels");
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

        public static HtmlTag Link<TController>(this HtmlHelper helper, Expression<Action<TController>> action) where TController : Controller
        {
            var linkText = ((MethodCallExpression)action.Body).Method.Name;

            return helper.Link(action, linkText);
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

        private static IElementGenerator<T> GetGenerator<T>(T model) where T : class
        {
            var library =
                StructuremapMvc.ParentScope.CurrentNestedContainer.GetInstance<HtmlConventionLibrary>();
            return ElementGenerator<T>.For(library, t => StructuremapMvc.ParentScope.CurrentNestedContainer.GetInstance(t), model);
        }
    }
}
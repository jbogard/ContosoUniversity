namespace ContosoUniversity.Infrastructure.Tags
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Web.UI;
    using HtmlTags.Conventions;
    using HtmlTags.UI;
    using HtmlTags.UI.Elements;

    public class DefaultAspNetMvcHtmlConventions : HtmlConventionRegistry
    {
        public DefaultAspNetMvcHtmlConventions()
        {
            Editors.Always.AddClass("form-control");

            Editors.IfPropertyIs<DateTime?>().ModifyWith(m => m.CurrentTag
                .AddPattern("9{1,2}/9{1,2}/9999")
                .AddPlaceholder("MM/DD/YYYY")
                .AddClass("datepicker")
                .Value(m.Value<DateTime?>() != null ? m.Value<DateTime>().ToShortDateString() : string.Empty));

            Labels.Always.AddClass("control-label");
            Labels.Always.AddClass("col-md-2");
            Labels.ModifyForAttribute<DisplayAttribute>((t, a) => t.Text(a.Name));
            Editors.BuilderPolicy<InstructorSelectElementBuilder>();
            DisplayLabels.Always.BuildBy<DefaultDisplayLabelBuilder>();
            DisplayLabels.ModifyForAttribute<DisplayAttribute>((t, a) => t.Text(a.Name));
            Displays.IfPropertyIs<DateTime>().ModifyWith(m => m.CurrentTag.Text(m.Value<DateTime>().ToShortDateString()));
            Displays.IfPropertyIs<decimal>().ModifyWith(m => m.CurrentTag.Text(m.Value<decimal>().ToString("C")));
        }

        public ElementCategoryExpression DisplayLabels
        {
            get { return new ElementCategoryExpression(Library.For<ElementRequest>().Category("DisplayLabels").Profile(TagConstants.Default)); }
        }

    }
}
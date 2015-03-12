namespace ContosoUniversity.Infrastructure.Tags
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using HtmlTags.UI;

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
            //Editors.Modifier<RequiredFieldModifier>();
            //Labels.Modifier<RequiredFieldModifier>();
        }
    }
}
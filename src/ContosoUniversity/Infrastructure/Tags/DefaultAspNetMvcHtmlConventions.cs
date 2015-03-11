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
            //Editors.IfPropertyHasAttribute<TextAreaAttribute>()
            //    .ModifyWith(m => m.ReplaceTag(new HtmlTag("textarea").Text(m.Value<string>()).AddClass("form-control").Attr("rows", 3)));

            Editors.IfPropertyIs<DateTime?>().ModifyWith(m => m.CurrentTag
                .AddPattern("9{1,2}/9{1,2}/9999")
                .AddPlaceholder("MM/DD/YYYY")
                .AddClass("datepicker")
                .Value(m.Value<DateTime?>() != null ? m.Value<DateTime>().ToShortDateString() : string.Empty));

            Labels.Always.AddClass("control-label");
            Labels.Always.AddClass("col-md-2");
            Labels.ModifyForAttribute<DisplayAttribute>((t, a) => t.Text(a.Name));
            Editors.BuilderPolicy<InstructorSelectElementBuilder>();
            //Editors.BuilderPolicy<PlantMultiselectElementBuilder>();
            //Editors.BuilderPolicy<ResourceMultiselectElementBuilder>();
            //Editors.BuilderPolicy<MesOperationStatusMultiselectElementBuilder>();
            //Editors.BuilderPolicy<OrderTypeMultiselectElementBuilder>();
            //Editors.Modifier<RequiredFieldModifier>();
            //Labels.Modifier<RequiredFieldModifier>();
        }
    }
}
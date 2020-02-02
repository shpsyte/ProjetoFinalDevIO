using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace App.Helpers {
    public class EmailTagHelper : TagHelper {

        public string domain { get; set; }
        public override async Task ProcessAsync (TagHelperContext context, TagHelperOutput output) {
            output.TagName = "a";
            var content = await output.GetChildContentAsync ();
            var target = content.GetContent () + "@" + (domain?.Length > 0 ? domain : "gmail.com");
            output.Attributes.SetAttribute ("href", "mailto:" + target);
            output.Attributes.SetAttribute ("target", "_blank");
            output.Content.SetContent (target);

        }

    }
}
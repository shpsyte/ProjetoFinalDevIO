using System;
using Microsoft.AspNetCore.Mvc.Razor;

namespace App.Extensions {
    public static class RazorExtensions {
        public static string FormataDoc (this RazorPage page, int tipo, string value) {
            var formato = Convert.ToUInt64 (value).ToString (@"000\.000\.000\-00");
            return formato;
        }

    }
}
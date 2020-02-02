using System;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Razor;

namespace App.Extensions {
    public static class RazorExtensions {
        public static string FormataDoc (this RazorPage page, int tipo, string value) {
            var formato = Convert.ToUInt64 (value).ToString (@"000\.000\.000\-00");
            return formato;
        }
        public static bool IfClaim (this RazorPage page, string claimName, string claimValue) {
            return CustomAutorization.ValidaClaimsUsuario (page.Context, claimName, claimValue);
        }

        public static IHtmlContent IfClaim (this IHtmlContent page, HttpContext context, string claimName, string claimValue) {
            return CustomAutorization.ValidaClaimsUsuario (context, claimName, claimValue) ? page : null;

        }

    }
}
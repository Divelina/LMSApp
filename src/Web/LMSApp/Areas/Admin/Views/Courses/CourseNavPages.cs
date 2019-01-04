using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LMSApp.Areas.Identity.Pages.Account.Manage
{
    public static class CourseNavPages
    {
        public static string All => "All";

        public static string Create => "Create";

        public static string AllNavClass(ViewContext viewContext) => PageNavClass(viewContext, All);

        public static string CreateNavClass(ViewContext viewContext) => PageNavClass(viewContext, Create);
    
        public static string PageNavClass(ViewContext viewContext, string page)
        {
            //var activePage = viewContext.ViewData["ActivePage"] as string
            //    ?? System.IO.Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);
            var activePage = viewContext.RouteData.Values["Action"] as string
                ?? System.IO.Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
        }
    }
}

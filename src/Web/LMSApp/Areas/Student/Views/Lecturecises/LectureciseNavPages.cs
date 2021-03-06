﻿using System;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LMSApp.Areas.Student.Views.Lecturecises
{
    public static class LectureciseNavPages
    {
        public static string All => "All";

        public static string AllLecturecises => "AllLecturecises";

        public static string CurrentYearLecturecises => "CurrentYearLecturecises";

        public static string AllNavClass(ViewContext viewContext) => PageNavClass(viewContext, All);

        public static string AllLecturecisesNavClass(ViewContext viewContext) => PageNavClass(viewContext, AllLecturecises);

        public static string CurrentYearLecturecisesNavClass(ViewContext viewContext) => PageNavClass(viewContext, CurrentYearLecturecises);

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

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LMSApp.Areas.Identity.Pages.Account.Manage
{
    public static class UsersNavPages
    {
        public static string All => "All";
        public static string AllStudents => "AllStudents";
        public static string AddStudentsToCourse => "AddStudentsToCourse";

        public static string AllNavClass(ViewContext viewContext) => PageNavClass(viewContext, All);
        public static string AllStudentsNavClass(ViewContext viewContext) => PageNavClass(viewContext, AllStudents);
        public static string AddStudentsToCourseNavClass(ViewContext viewContext) => PageNavClass(viewContext, AddStudentsToCourse);

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

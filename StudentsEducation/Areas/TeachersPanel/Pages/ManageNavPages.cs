using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace StudentsEducation.Web.Areas.TeachersPanel.Pages
{
    public static class ManageNavPages
    {
        public static string Index => "Index";

        public static string Marks => "Marks";
        public static string Deadlines => "Deadlines";
        public static string Skips => "Skips";

        public static string IndexNavClass(ViewContext viewContext) => PageNavClass(viewContext, Index);

        public static string MarksNavClass(ViewContext viewContext) => PageNavClass(viewContext, Marks);

        public static string DeadlinesNavClass(ViewContext viewContext) => PageNavClass(viewContext, Deadlines);

        public static string SkipsNavClass(ViewContext viewContext) => PageNavClass(viewContext, Skips);
        private static string PageNavClass(ViewContext viewContext, string page)
        {
            var activePage = viewContext.ViewData["ActivePage"] as string
                ?? System.IO.Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
        }
    }
}

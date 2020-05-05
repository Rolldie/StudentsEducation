using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace StudentsEducation.Web.Areas.Admin.Pages
{
    public class IndexModel : PageModel
    {
        public Dictionary<string, string> PageAndName { get; set; }
        public void OnGet()
        {
            PageAndName = new Dictionary<string, string>();
            PageAndName.Add("./Users/Index", "User");
            PageAndName.Add("./Roles/Index", "Roles");
            PageAndName.Add("./Students/Index", "Students");
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;

namespace StudentsEducation.Web.Areas.Admin.Pages
{
    public class Index1Model : PageModel
    {

        private readonly RoleManager<IdentityUser> roleManager;
        public Index1Model(RoleManager<IdentityUser> manager)
        {
            roleManager = manager;
        }


        public void OnGet()
        {


        }
    }
}

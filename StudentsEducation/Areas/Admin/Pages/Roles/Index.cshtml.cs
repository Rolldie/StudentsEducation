using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StudentsEducation.Infrastructure.Identity;
using StudentsEducation.Infrastructure.Identity.Data;

namespace StudentsEducation.Web.Areas.Admin.Pages.Roles
{
    public class IndexModel : PageModel
    {
        private readonly RoleManager<Role> _roleManager;

        public IndexModel(RoleManager<Role> roleManager)
        {
            _roleManager = roleManager;
        }

        public IList<Role> Role { get;set; }

        public async Task OnGetAsync()
        {
            Role = await _roleManager.Roles.ToListAsync();
        }
    }
}

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
using StudentsEducation.Web.Areas.Admin.Pages.Users;

namespace StudentsEducation.Web.Areas.Admin.Pages.Roles
{
    public class DetailsModel : PageModel
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        public DetailsModel(RoleManager<Role> roleManager, UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public Role Role { get; set; }
        public IEnumerable<AppUser> Users { get; set; }
        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Role = await _roleManager.Roles.FirstOrDefaultAsync(m => m.Id == id);

            Users = await _userManager.GetUsersInRoleAsync(Role.Name);

            if (Role == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}

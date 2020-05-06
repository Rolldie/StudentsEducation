using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentsEducation.Infrastructure.Identity;
using StudentsEducation.Infrastructure.Identity.Data;

namespace StudentsEducation.Web.Areas.Admin.Pages.Roles
{
    public class EditModel : PageModel
    {
        private readonly RoleManager<Role> _roleManager;

        public EditModel(RoleManager<Role> roleManager)
        {
            _roleManager = roleManager;
        }

        [BindProperty]
        public Role Role { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Role = await _roleManager.Roles.FirstOrDefaultAsync(m => m.Id == id);

            if (Role == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || !RoleExists(Role.Id))
            {
                return Page();
            }
            var curRole = await _roleManager.FindByIdAsync(Role.Id);
            curRole.Name = Role.Name;
            curRole.Description = Role.Description;
            await _roleManager.UpdateAsync(curRole);

            return RedirectToPage("./Index");
        }

        private bool RoleExists(string id)
        {
            return _roleManager.Roles.Any(e => e.Id == id);
        }
    }
}

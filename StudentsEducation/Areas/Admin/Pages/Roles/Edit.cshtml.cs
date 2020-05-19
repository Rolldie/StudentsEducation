using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentsEducation.Web.Areas.Account.Data;
using StudentsEducation.Web.Areas.Account.Services;

namespace StudentsEducation.Web.Areas.Admin.Pages.Roles
{
    public class EditModel : PageModel
    {
        private readonly IdentityService _service;

        public EditModel(IdentityService service)
        {
            _service = service;
        }

        [BindProperty]
        public Role Role { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Role = await _service.GetRoleAsync(id);

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
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var curRole = await _service.GetRoleAsync(Role.Id);
            if(curRole==null)
            {
                return Page();
            }
            curRole.Name = Role.Name;
            curRole.Description = Role.Description;
            curRole.IsDatabaseFieldsRequired = Role.IsDatabaseFieldsRequired;
            await _service.UpdateRoleAsync(curRole);

            return RedirectToPage("./Index");
        }
    }
}

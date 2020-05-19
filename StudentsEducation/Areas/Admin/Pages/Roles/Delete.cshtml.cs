using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentsEducation.Web.Areas.Account.Data;
using StudentsEducation.Web.Areas.Account.Services;

namespace StudentsEducation.Web.Areas.Admin.Pages.Roles
{
    public class DeleteModel : PageModel
    {
        private readonly IdentityService _service;

        public DeleteModel(IdentityService service)
        {
            _service = service;
        }

        [BindProperty]
        public Role Role { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return RedirectToPage(Url.Content("./Index"));
            }

            Role = await _service.GetRoleAsync(id);

            if (Role == null)
            {
                return RedirectToPage(Url.Content("./Index"));
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return RedirectToPage(Url.Content("./Index"));
            }
            var role = await _service.GetRoleAsync(id);
            if (role.Name == "Administrator") return RedirectToPage(Url.Content("~/Error"));

            await _service.DeleteRoleAsync(id);

            return RedirectToPage(Url.Content("./Index"));
        }
    }
}

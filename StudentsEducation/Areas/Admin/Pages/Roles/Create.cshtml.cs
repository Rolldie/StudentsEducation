using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentsEducation.Web.Areas.Account.Data;
using StudentsEducation.Web.Areas.Account.Services;

namespace StudentsEducation.Web.Areas.Admin.Pages.Roles
{
    public class CreateModel : PageModel
    {
        private readonly IdentityService _service;

        public CreateModel(IdentityService service)
        {
            _service = service;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Role Role { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _service.CreateRoleAsync(Role);

            return RedirectToPage("./Index");
        }
    }
}

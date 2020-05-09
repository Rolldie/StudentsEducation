using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentsEducation.Infrastructure.Identity.Data;
using StudentsEducation.Infrastructure.Services;

namespace StudentsEducation.Web.Areas.Admin.Pages.Roles
{
    public class DetailsModel : PageModel
    {
        private readonly IdentityService _service;
        public DetailsModel(IdentityService service)
        {
            _service = service;
        }

        public Role Role { get; set; }
        public IEnumerable<AppUser> Users { get; set; }
        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Role = await _service.GetRoleAsync(id);

            Users = await _service.GetUsersByRoleAsync(id);

            if (Role == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}

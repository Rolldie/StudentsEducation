using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StudentsEducation.Infrastructure.Identity;
using StudentsEducation.Infrastructure.Identity.Data;

namespace StudentsEducation.Web.Areas.Admin.Pages.Roles
{
    public class DetailsModel : PageModel
    {
        private readonly StudentsEducation.Infrastructure.Identity.AccountDbContext _context;

        public DetailsModel(StudentsEducation.Infrastructure.Identity.AccountDbContext context)
        {
            _context = context;
        }

        public Role Role { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Role = await _context.Roles.FirstOrDefaultAsync(m => m.Id == id);

            if (Role == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}

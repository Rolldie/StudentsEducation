using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StudentsEducation.Domain.Entities;
using StudentsEducation.Infrastructure.Data;

namespace StudentsEducation.Web.Areas.Admin.Pages.Students.Skips
{
    public class DeleteModel : PageModel
    {
        private readonly StudentsEducation.Infrastructure.Data.EducationDbContext _context;

        public DeleteModel(StudentsEducation.Infrastructure.Data.EducationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Skip Skip { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Skip = await _context.Skips
                .Include(s => s.Schedule)
                .Include(s => s.Student).FirstOrDefaultAsync(m => m.Id == id);

            if (Skip == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Skip = await _context.Skips.FindAsync(id);

            if (Skip != null)
            {
                _context.Skips.Remove(Skip);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index", new {id=Skip.StudentId});
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StudentsEducation.Domain.Entities;
using StudentsEducation.Infrastructure.Data;

namespace StudentsEducation.Web.Areas.Admin.Pages.Students.FinalControls
{
    public class DeleteModel : PageModel
    {
        private readonly StudentsEducation.Infrastructure.Data.EducationDbContext _context;

        public DeleteModel(StudentsEducation.Infrastructure.Data.EducationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public FinalControl FinalControl { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            FinalControl = await _context.FinalControls
                .Include(f => f.Student)
                .Include(f => f.Subject).FirstOrDefaultAsync(m => m.Id == id);

            if (FinalControl == null)
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

            FinalControl = await _context.FinalControls.FindAsync(id);

            if (FinalControl != null)
            {
                _context.FinalControls.Remove(FinalControl);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index",new { id = FinalControl.StudentId });
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StudentsEducation.Domain.Entities;
using StudentsEducation.Infrastructure.Data;

namespace StudentsEducation.Web.Areas.Admin.Pages.Students.Marks
{
    public class DeleteModel : PageModel
    {
        private readonly StudentsEducation.Infrastructure.Data.EducationDbContext _context;

        public DeleteModel(StudentsEducation.Infrastructure.Data.EducationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Mark Mark { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Mark = await _context.Marks
                .Include(m => m.Student)
                .Include(m => m.Work).FirstOrDefaultAsync(m => m.Id == id);

            if (Mark == null)
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

            Mark = await _context.Marks.FindAsync(id);

            if (Mark != null)
            {
                _context.Marks.Remove(Mark);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index",new { id = Mark.StudentId });
        }
    }
}

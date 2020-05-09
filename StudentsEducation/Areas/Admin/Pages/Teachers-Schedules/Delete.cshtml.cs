using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StudentsEducation.Domain.Entities;
using StudentsEducation.Infrastructure.Data;

namespace StudentsEducation.Web.Areas.Admin.Pages.Teachers_Schedules
{
    public class DeleteModel : PageModel
    {
        private readonly StudentsEducation.Infrastructure.Data.EducationDbContext _context;

        public DeleteModel(StudentsEducation.Infrastructure.Data.EducationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Teacher Teacher { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Teacher = await _context.Teachers.FirstOrDefaultAsync(m => m.Id == id);

            if (Teacher == null)
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

            Teacher = await _context.Teachers.FindAsync(id);

            if (Teacher != null)
            {
                _context.Teachers.Remove(Teacher);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}

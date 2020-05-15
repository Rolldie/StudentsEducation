using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using StudentsEducation.Domain.Entities;
using StudentsEducation.Infrastructure.Data;

namespace StudentsEducation.Web.Areas.Admin.Pages.Teachers_Schedules
{
    public class CreateModel : PageModel
    {
        private readonly StudentsEducation.Infrastructure.Data.EducationDbContext _context;

        public CreateModel(StudentsEducation.Infrastructure.Data.EducationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Teacher Teacher { get; set; }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Teachers.Add(Teacher);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StudentsEducation.Domain.Entities;

namespace StudentsEducation.Web.Areas.Admin.Pages.Students
{
    public class IndexModel : PageModel
    {
        private readonly StudentsEducation.Infrastructure.Data.EducationDbContext _context;

        public IndexModel(StudentsEducation.Infrastructure.Data.EducationDbContext context)
        {
            _context = context;
        }

        public IList<Student> Student { get;set; }
        public async Task OnGetAsync()
        {
            Student = await _context.Students.ToListAsync();
        }
    }
}

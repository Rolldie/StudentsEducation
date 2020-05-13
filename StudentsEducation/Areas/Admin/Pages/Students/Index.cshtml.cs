using System.Collections.Generic;
using System.Linq;
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
        [Microsoft.AspNetCore.Mvc.BindProperty(SupportsGet =true)]
        public string SearchQuery { get; set; }
        public IList<Student> Students { get;set; }
        public async Task OnGetAsync()
        {
            Students = await _context.Students.ToListAsync();
            if(!string.IsNullOrEmpty(SearchQuery))
            {
                Students = Students.Where(e => e.Group.Name.ToUpper().Contains(SearchQuery.ToUpper())).ToList();
            }
        }
    }
}

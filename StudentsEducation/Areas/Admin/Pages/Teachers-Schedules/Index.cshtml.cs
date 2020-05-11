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
    public class IndexModel : PageModel
    {
        private readonly StudentsEducation.Infrastructure.Data.EducationDbContext _context;

        public IndexModel(StudentsEducation.Infrastructure.Data.EducationDbContext context)
        {
            _context = context;
        }
        
        
        [BindProperty(SupportsGet =true)]
        public string SearchQuery { get; set; }
        public IList<Teacher> Teacher { get;set; }

        public async Task OnGetAsync()
        {
            if (!string.IsNullOrEmpty(SearchQuery))
                Teacher = (await _context.Teachers.ToListAsync()).Where(e => e.Name.ToUpper().Contains(SearchQuery.ToUpper())).ToList();
            else
                Teacher = await _context.Teachers.ToListAsync();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StudentsEducation.Infrastructure.Identity;
using StudentsEducation.Infrastructure.Identity.Data;

namespace StudentsEducation.Web.Areas.Admin.Pages.Users_relation
{
    public class IndexModel : PageModel
    {
        private readonly StudentsEducation.Infrastructure.Identity.AccountDbContext _context;

        public IndexModel(StudentsEducation.Infrastructure.Identity.AccountDbContext context)
        {
            _context = context;
        }

        public IList<AppUser> Users { get;set; }

        public async Task OnGetAsync()
        {
            Users = await _context.Users.ToListAsync();
        }
    }
}

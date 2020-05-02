using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace StudentsEducation.Web.Areas.Admin.Pages.Users
{
    public class IndexModel : PageModel
    {

        private readonly UserManager<IdentityUser> userManager;
        public IndexModel(UserManager<IdentityUser> manager)
        {
            userManager = manager;
        }
        public List<IdentityUser> Users { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Users = await userManager.Users.ToListAsync();
            return Page();
        }
    }
}
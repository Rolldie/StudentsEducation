using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace StudentsEducation.Web.Areas.Account.Pages
{
    public class TeacherModel : PageModel
    {
        public async Task<IActionResult> OnGetAsync(string userId)
        {
            if(string.IsNullOrEmpty(userId))
            {
                return NotFound();
            }



            return Page();
        }
    }
}
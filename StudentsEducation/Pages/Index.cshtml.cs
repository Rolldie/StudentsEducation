using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentsEducation.Domain.Entities;
using StudentsEducation.Domain.Interfaces;

namespace StudentsEducation.Web.Pages
{
    [Authorize]
    public class IndexModel : PageModel
    {
        public IndexModel(ICathedrasAndGroupsService service)
        {
        }
        public IActionResult OnGet()
        {
            return Page();
        }
    }
}
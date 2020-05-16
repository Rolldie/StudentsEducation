using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace StudentsEducation.Web.Areas.TeachersPanel.Pages.Students
{
    public class MarkEditModel : PageModel
    {
        public bool AddMode { get; set; }
        public async Task<IActionResult> OnGetAsync(int ?workId, int ?stId)
        {
            AddMode = true;
            if (!workId.HasValue || !stId.HasValue)
            { }



             return Page();
        }
    }
}
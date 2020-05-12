using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StudentsEducation.Domain.Entities;
using StudentsEducation.Domain.Interfaces;

namespace StudentsEducation.Web.Areas.Admin.Pages.Subjects_Works
{
    public class DeleteModel : PageModel
    {
        private readonly ISubjectAndWorksService _service;

        public DeleteModel(ISubjectAndWorksService service)
        {
            _service = service;
        }

        [BindProperty]
        public Subject Subject { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return RedirectToPage(Url.Content("./Index"));
            }

            Subject = await _service.GetSubjectAsync(id.Value);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return RedirectToPage(Url.Content("./Index"));
            }

            await _service.DeleteSubjectAsync(id.Value);

            return RedirectToPage(Url.Content("./Index"));
        }
    }
}

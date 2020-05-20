using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentsEducation.Domain.Entities;
using StudentsEducation.Domain.Interfaces;

namespace StudentsEducation.Web.Areas.Admin.Pages.Cathedras_Groups
{
    public class CreateModel : PageModel
    {
        private readonly ICathedrasAndGroupsService _service;

        public CreateModel(ICathedrasAndGroupsService service)
        {
            _service = service;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Cathedra Cathedra { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _service.CreateCathedraAsync(Cathedra);
            
            return RedirectToPage("./Index");
        }
    }
}

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentsEducation.Domain.Entities;
using StudentsEducation.Domain.Interfaces;

namespace StudentsEducation.Web.Areas.Admin.Pages.Cathedras_Groups
{
    public class DeleteModel : PageModel
    {
        private readonly ICathedrasAndGroupsService _service;

        public DeleteModel(ICathedrasAndGroupsService service)
        {
            _service = service;
        }

        [BindProperty]
        public Cathedra Cathedra { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Cathedra = await _service.GetCathedraByIdAsync(id.Value);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await _service.DeleteCathedraAsync(id.Value);

            return RedirectToPage("./Index");
        }
    }
}

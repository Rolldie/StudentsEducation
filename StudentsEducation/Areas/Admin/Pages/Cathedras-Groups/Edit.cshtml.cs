using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StudentsEducation.Domain.Entities;
using StudentsEducation.Domain.Interfaces;

namespace StudentsEducation.Web.Areas.Admin.Pages.Cathedras_Groups
{
    public class EditModel : PageModel
    {
        private readonly ICathedrasAndGroupsService _service;

        public EditModel(ICathedrasAndGroupsService service)
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

            if (Cathedra == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                await _service.UpdateCathedraAsync(Cathedra);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CathedraExists(Cathedra.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool CathedraExists(int id)
        {
            return _service.GetCathedraByIdAsync(id) != null;
        }
    }
}

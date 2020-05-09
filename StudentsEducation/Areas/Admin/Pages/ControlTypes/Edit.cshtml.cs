using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StudentsEducation.Domain.Entities;
using StudentsEducation.Domain.Interfaces;

namespace StudentsEducation.Web.Areas.Admin.Pages.ControlTypes
{
    public class EditModel : PageModel
    {
        private readonly IAsyncRepository<ControlType> _repository;

        public EditModel(IAsyncRepository<ControlType> repository)
        {
            _repository = repository;
        }

        [BindProperty]
        public ControlType ControlType { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ControlType = await _repository.GetByIdAsync(id.Value);

            if (ControlType == null)
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
                await _repository.UpdateAsync(ControlType);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ControlTypeExists(ControlType.Id))
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

        private bool ControlTypeExists(int id)
        {
            return _repository.GetById(id)!=null;
        }
    }
}

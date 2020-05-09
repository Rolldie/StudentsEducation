using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using StudentsEducation.Domain.Entities;
using StudentsEducation.Domain.Interfaces;

namespace StudentsEducation.Web.Areas.Admin.Pages.Subjects_Works
{
    public class CreateModel : PageModel
    {
        private readonly ISubjectAndWorksService _service;

        public CreateModel(ISubjectAndWorksService service)
        {
            _service = service;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            ControlTypes = await _service.GetControlTypesAsync();

            List = new SelectList(ControlTypes, "Id", "ControlName");
            return Page();
        }
        public IEnumerable<ControlType> ControlTypes { get; set; }
        [BindProperty]
        public SelectList List { get; set; }

        [BindProperty]
        public Subject Subject { get; set; }
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Subject.ControlType = await _service.GetControlTypeAsync((int)List.SelectedValue);
            await _service.AddNewSubjectAsync(Subject);

            return RedirectToPage("./Index");
        }
    }
}

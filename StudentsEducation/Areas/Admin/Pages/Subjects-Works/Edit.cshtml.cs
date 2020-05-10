using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentsEducation.Domain.Entities;
using StudentsEducation.Domain.Interfaces;

namespace StudentsEducation.Web.Areas.Admin.Pages.Subjects_Works
{
    public class EditModel : PageModel
    {
        private readonly ISubjectAndWorksService _service;

        public EditModel(ISubjectAndWorksService service)
        {
            _service = service;
        }

        [BindProperty]
        public Subject Subject { get; set; }
        public SelectList Items { get; set; }


        private async Task InitLists()
        {
            Items = new SelectList(await _service.GetControlTypesAsync(), "Id", "ControlName",SelectedControlType);
        }
        [BindProperty]
        [Required(ErrorMessage ="Выберите тип контроля!")]
        public string SelectedControlType { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Subject = await _service.GetSubjectAsync(id.Value);
            await InitLists();
            SelectedControlType = Subject.ControlType.Id.ToString();
            if (Subject == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if(SelectedControlType!=null)
            Subject.ControlType = await _service.GetControlTypeAsync(int.Parse(SelectedControlType));
            ModelState.Remove("Subject.ControlType");
            if (!ModelState.IsValid)
            {
                await InitLists();
                return Page();
            }


            try
            {
                await _service.UpdateSubjectAsync(Subject);
            }
            catch (DbUpdateConcurrencyException)
            {
                await InitLists();
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}

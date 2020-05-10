using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.V3.Pages.Account.Manage.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;
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
            await InitListsAsync();
            return Page();
        }
        public IEnumerable<ControlType> ControlTypes { get; set; }
        public SelectList List { get; set; }
        [BindProperty]
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage ="Выберите тип контроля!")]
        public string SelectedValue { get; set; }
        private async Task InitListsAsync()
        {
            ControlTypes = await _service.GetControlTypesAsync();

            List = new SelectList(ControlTypes, "Id", "ControlName",SelectedValue);
        }

        [BindProperty]
        public Subject Subject { get; set; }
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            ModelState.Remove("Subject.ControlType");
            if(!ModelState.IsValid)
            {
                await InitListsAsync();
                return Page();
            }
            Subject.ControlType = await _service.GetControlTypeAsync(int.Parse(SelectedValue));
            if(Subject.ControlType!=null)
            await _service.AddNewSubjectAsync(Subject);
            else
            {
                return Page();
            }
            return RedirectToPage("./Index");
        }
    }
}

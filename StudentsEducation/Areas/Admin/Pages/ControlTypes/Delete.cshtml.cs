using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StudentsEducation.Domain.Entities;
using StudentsEducation.Domain.Interfaces;
using StudentsEducation.Infrastructure.Data;

namespace StudentsEducation.Web.Areas.Admin.Pages.ControlTypes
{
    public class DeleteModel : PageModel
    {
        private readonly IAsyncRepository<ControlType> _repository;

        public DeleteModel(IAsyncRepository<ControlType> repository)
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await _repository.DeleteAsync(id.Value);

            return RedirectToPage("./Index");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentsEducation.Domain.Entities;
using StudentsEducation.Domain.Interfaces;

namespace StudentsEducation.Web.Areas.TeachersPanel.Pages.Raitings
{
    public class ForCathedraModel : PageModel
    {
        private readonly ICathedrasAndGroupsService _cathService;
        private readonly IStudentsService _studService;
        public ForCathedraModel(ICathedrasAndGroupsService cathService)
        {
            _cathService = cathService;
        }
        public IEnumerable<Cathedra> Cathedras { get; set; }

        public async Task<IActionResult>OnGetAsync(int?CathedraId)
        {
            if (!CathedraId.HasValue) return NotFound();
            Cathedras = await _cathService.GetCathedrasAsync();
            return Page();

        }
    }
}
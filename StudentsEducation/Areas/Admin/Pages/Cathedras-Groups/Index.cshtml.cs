using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentsEducation.Domain.Entities;
using StudentsEducation.Domain.Interfaces;

namespace StudentsEducation.Web.Areas.Admin.Pages.Cathedras_Groups
{
    public class IndexModel : PageModel
    {
        private readonly ICathedrasAndGroupsService _service;

        public IndexModel(ICathedrasAndGroupsService service)
        {
            _service = service;
        }

        public IEnumerable<Cathedra> Cathedra { get;set; }
        public IEnumerable<Student> Students { get; set; }
        public async Task OnGetAsync()
        {
            Cathedra = await _service.GetCathedrasAsync();

        }
    }
}

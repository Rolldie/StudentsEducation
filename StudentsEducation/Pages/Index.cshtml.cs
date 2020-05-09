using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentsEducation.Domain.Entities;
using StudentsEducation.Domain.Interfaces;

namespace StudentsEducation.Web.Pages
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ICathedrasAndGroupsService _service;
        public IndexModel(ICathedrasAndGroupsService service)
        {
            _service = service;
        }

        public IEnumerable<Group> ListGroups { get; set; }
        public async Task OnGetAsync()
        {
            ListGroups = await _service.GetCatherdaGroupsAsync(1);
        }
    }
}
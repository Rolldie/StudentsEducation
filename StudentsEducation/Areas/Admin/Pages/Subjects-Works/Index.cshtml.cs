using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StudentsEducation.Domain.Entities;
using StudentsEducation.Domain.Interfaces;

namespace StudentsEducation.Web.Areas.Admin.Pages.Subjects_Works
{
    public class IndexModel : PageModel
    {
        private readonly ISubjectAndWorksService _service;

        public IndexModel(ISubjectAndWorksService service)
        {
            _service = service;
        }

        public IList<Subject> Subject { get;set; }

        public async Task OnGetAsync()
        {
            Subject = (await _service.GetSubjectsAsync()).ToList();
        }
    }
}

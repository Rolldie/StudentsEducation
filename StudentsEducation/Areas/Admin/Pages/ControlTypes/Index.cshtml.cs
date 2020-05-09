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
    public class IndexModel : PageModel
    {
        private readonly ISubjectAndWorksService _service;

        public IndexModel(ISubjectAndWorksService service)
        {
            _service = service;
        }

        public IList<ControlType> ControlType { get;set; }

        public async Task OnGetAsync()
        {
            ControlType = (await _service.GetControlTypesAsync()).ToList();
        }
    }
}

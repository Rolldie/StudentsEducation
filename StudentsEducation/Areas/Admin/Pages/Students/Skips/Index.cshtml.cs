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

namespace StudentsEducation.Web.Areas.Admin.Pages.Students.Skips
{
    public class IndexModel : PageModel
    {
        private readonly IStudentsService _service;

        public IndexModel(IStudentsService service)
        {
            _service = service;
        }

        public IEnumerable<Skip> Skips { get; set; }
        public Student Student { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }
            Student = await _service.GetStudentAsync(id.Value);
            if (Student == null) return NotFound();
            Skips = await _service.GetSkipsByStudentAsync(Student.Id);
            return Page();
        }
    }
}

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

namespace StudentsEducation.Web.Areas.Admin.Pages.Teachers_Schedules
{
    public class DetailsModel : PageModel
    {
        private readonly ITeachersAndScheduleSerivce _service;

        public DetailsModel(ITeachersAndScheduleSerivce service)
        {
            _service = service;
        }

        public Teacher Teacher { get; set; }
        public IEnumerable<Schedule> Schedules { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Teacher = await _service.GetTeacherAsync(id.Value);
            if (Teacher == null)
            {
                return NotFound();
            }
            Schedules = await _service.GetSchedulesByTeacherAsync(id.Value);
            return Page();
        }
    }
}

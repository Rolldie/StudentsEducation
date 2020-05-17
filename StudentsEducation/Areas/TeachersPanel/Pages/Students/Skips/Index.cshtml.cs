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

namespace StudentsEducation.Web.Areas.TeachersPanel.Pages.Students.Skips
{
    public class IndexModel : PageModel
    {
        private readonly ITeachersAndScheduleSerivce _teachService;
        private readonly IStudentsService _stService;

        public IndexModel(ITeachersAndScheduleSerivce teacherService ,IStudentsService service)
        {
            _teachService = teacherService;
            _stService = service;
        }

        public IEnumerable<Skip> Skips { get; set; }
        public Schedule Schedule { get; set; }
        public Student Student { get; set; }
        public async Task<IActionResult> OnGetAsync(int? schId,int?studId)
        {
            if (!schId.HasValue)
            {
                return NotFound();
            }
            Schedule = await _teachService.GetScheduleAsync(schId.Value);
            if (Schedule == null) return NotFound();
            Student = await _stService.GetStudentAsync(studId.Value);
            if (Student == null) return NotFound();
            Skips = Student.Skips.Where(e => e.ScheduleId == Schedule.Id);
            return Page();
        }
    }
}

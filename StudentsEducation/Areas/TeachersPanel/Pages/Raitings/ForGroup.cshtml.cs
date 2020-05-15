using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentsEducation.Domain.Entities;
using StudentsEducation.Domain.Interfaces;

namespace StudentsEducation.Web.Areas.TeachersPanel.Pages.Raitings
{
    public class ForGroupModel : PageModel
    {
        private readonly ITeachersAndScheduleSerivce _teachService;
        private readonly IStudentsService _studService;
        public ForGroupModel(ITeachersAndScheduleSerivce teacherService,IStudentsService studentService)
        {
            _teachService = teacherService;
            _studService = studentService;
        }
        public Group Group { get; set; }
        public Teacher Teacher { get; set; }

        public IEnumerable<Schedule> Schedules { get; set; }

        public async Task<IActionResult> OnGetAsync(int? TeacherId,int? GroupId)
        {
            if (!GroupId.HasValue || !TeacherId.HasValue) return NotFound();
            Group = await _teachService.GetGroupAsync(GroupId.Value);
            Teacher = await _teachService.GetTeacherAsync(TeacherId.Value);
            if (Teacher == null || Group == null) return NotFound();
            Schedules = Teacher.Schedules.Intersect(Group.Schedules);
            return Page();
        }
        public async Task<double> GetAcademic(int student,int schedule)
        {
            return await _studService.GetAcademicPerfomanceAsync(student, schedule);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using StudentsEducation.Domain.Entities;
using StudentsEducation.Domain.Interfaces;
using StudentsEducation.Web.Areas.Account.Services;

namespace StudentsEducation.Web.Areas.TeachersPanel.Pages
{
    public class FinalMarksModel : PageModel
    {
        private readonly IdentityService _service;
        private readonly ITeachersAndScheduleSerivce _teachService;
        public FinalMarksModel(IdentityService service, ITeachersAndScheduleSerivce teacherService)
        {
            _service = service;
            _teachService = teacherService;
        }

        public Teacher Teacher { get; set; }

        [BindProperty(SupportsGet = true)]
        public int fCBy { get; set; }

        public Schedule ScheduleForView { get; set; }
        public class ChooseMarksList
        {
            public int ScheduleId { get; set; }
            public string Text { get; set; }
        }



        public IEnumerable<Schedule> Schedules { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _service.GetCurrentUser(HttpContext);
            if (string.IsNullOrEmpty(user.DbId))
                return RedirectToPage(Url.Content("~/Error"), new { ErrorMessage = "Ошибка, нет пользователя в Базе данных!" });
            int id = int.Parse(user.DbId);
            Teacher = await _teachService.GetTeacherAsync(id);
            if (Teacher == null)
                return RedirectToPage(Url.Content("~/Error"), new { ErrorMessage = "Ошибка, нет пользователя в Базе данных!" });
            ViewData["Teacher"] = Teacher.Name;
            Schedules = (await _teachService.GetSchedulesByTeacherAsync(Teacher.Id)).OrderByDescending(e=>e.EndsIn);
            ViewData["Schedules"] = new SelectList(Schedules.Select(e => new ChooseMarksList
            {
                ScheduleId = e.Id,
                Text = $"{e.Subject.Name} {e.Group.Name} {e.StartsIn.ToShortDateString()} {e.EndsIn.ToShortDateString()}"
            }), "ScheduleId", "Text");
            if (fCBy > 0)
            {
                ScheduleForView = Schedules.FirstOrDefault(e => e.Id == fCBy);
            }
            return Page();
        }

        public async Task<double> GetFinalControl(Student student)
        {
           var finalcontrol = student.FinalControls.FirstOrDefault(e => e.Schedule.SubjectId == ScheduleForView.SubjectId);
            return finalcontrol != null ? finalcontrol.MarkValue : 0;
        }
    }
}
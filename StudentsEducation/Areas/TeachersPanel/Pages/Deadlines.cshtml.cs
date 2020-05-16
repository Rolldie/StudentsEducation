using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using StudentsEducation.Domain.Entities;
using StudentsEducation.Domain.Interfaces;
using StudentsEducation.Infrastructure.Services;

namespace StudentsEducation.Web.Areas.TeachersPanel.Pages
{
    public class DeadlinesModel : PageModel
    {
        private readonly IdentityService _service;
        private readonly ITeachersAndScheduleSerivce _teachService;
        private readonly ISubjectAndWorksService _subjService;
        private readonly ICathedrasAndGroupsService _cathService;
        public DeadlinesModel(ICathedrasAndGroupsService cathService, IdentityService service, ITeachersAndScheduleSerivce teacherService, ISubjectAndWorksService subjectsService)
        {
            _cathService = cathService;
            _service = service;
            _teachService = teacherService;
            _subjService = subjectsService;
        }

        public Teacher Teacher { get; set; }

        [BindProperty(SupportsGet = true)]
        public int MarksBy { get; set; }
        public class ChooseMarksList
        {
            public int ScheduleId { get; set; }
            public string Text { get; set; }
        }

        public Schedule ScheduleForView { get; set; }

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
            Schedules = await _teachService.GetSchedulesByTeacherAsync(Teacher.Id);
            ViewData["Schedules"] = new SelectList(Schedules.Select(e => new ChooseMarksList
            {
                ScheduleId = e.Id,
                Text = e.Subject.Name + " " + e.Group.Name
            }), "ScheduleId", "Text");
            if(MarksBy>0)
            {
                ScheduleForView = Schedules.FirstOrDefault(e => e.Id == MarksBy);
            }
            return Page();
        }
    }
}
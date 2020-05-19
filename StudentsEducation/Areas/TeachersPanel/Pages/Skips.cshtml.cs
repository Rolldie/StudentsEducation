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
    public class SkipsModel : PageModel
    {
        private readonly IdentityService _service;
        private readonly ITeachersAndScheduleSerivce _teachService;
        private readonly ICathedrasAndGroupsService _cathService;
        public SkipsModel(ICathedrasAndGroupsService cathService, IdentityService service, ITeachersAndScheduleSerivce teacherService)
        {
            _cathService = cathService;
            _service = service;
            _teachService = teacherService;
        }

        public class ShowModel
        {
            public int Id { get; set; }
            public string Text { get; set; }
        }

        [BindProperty(SupportsGet = true)]
        public int SkipsFor { get; set; }

        public IEnumerable<Subject> SubjectsForView { get; set; }
        public IEnumerable<Group> Groups { get; set; }

        public Group GroupForView { get; set; }
        public Teacher Teacher { get; set; }
        public class ChooseMarksList
        {
            public int GroupId { get; set; }
            public string Text { get; set; }
        }

        public async Task<IActionResult> OnGetAsync(int ?SkipsFor)
        {
            if (SkipsFor.HasValue) this.SkipsFor = SkipsFor.Value;
            var user = await _service.GetCurrentUser(HttpContext);
            if (string.IsNullOrEmpty(user.DbId))
                return RedirectToPage(Url.Content("~/Error"), new { ErrorMessage = "Ошибка, нет пользователя в Базе данных!" });
            int id = int.Parse(user.DbId);
            Teacher = await _teachService.GetTeacherAsync(id);
            if (Teacher == null)
                return RedirectToPage(Url.Content("~/Error"), new { ErrorMessage = "Ошибка, нет пользователя в Базе данных!" });
            ViewData["Teacher"] = Teacher.Name;
            Groups = await _teachService.GetTeachersGroups(Teacher.Id);
            ViewData["Groups"] = new SelectList(Groups.Select(e => new ChooseMarksList
            {
                GroupId = e.Id,
                Text = e.Name + " " + e.StartEducationDate.ToShortDateString()+ " "+e.EndEducationDate.ToShortDateString()
            }), "GroupId", "Text");
            if (SkipsFor > 0)
            {
                GroupForView = Groups.FirstOrDefault(e => e.Id == SkipsFor);
                SubjectsForView = GroupForView.Schedules.Where(e => e.TeacherId == Teacher.Id).Select(e => e.Subject);
            }
            return Page();
        }
        public async Task<string> GetSkipsByAsync(int scheduleId,int studentId)
        {
            var schedule = await _teachService.GetScheduleAsync(scheduleId);
            var student = schedule.Group.Students.FirstOrDefault(e => e.Id == studentId);
            var studSkipsBySchedule = student.Skips.Where(e => e.ScheduleId == schedule.Id);
            double studSkips=studSkipsBySchedule.Count();
            double positiveSkips = studSkipsBySchedule.Count(e => e.IsGood);
            return $"{studSkips} на {positiveSkips} уваж.";
        }
    }
}
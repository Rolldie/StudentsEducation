using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using StudentsEducation.Domain.Entities;
using StudentsEducation.Domain.Interfaces;
using StudentsEducation.Infrastructure.Services;

namespace StudentsEducation.Web.Areas.TeachersPanel.Pages.Students
{
    public class FinalEditModel : PageModel
    {
        private readonly IdentityService _service;
        private readonly ITeachersAndScheduleSerivce _teachService;
        private readonly IStudentsService _stService;
        private readonly ISubjectAndWorksService _subjService;
        public FinalEditModel(ISubjectAndWorksService subjectsService, IdentityService service, ITeachersAndScheduleSerivce teacherService, IStudentsService studentsService)
        {
            _service = service;
            _teachService = teacherService;
            _stService = studentsService;
            _subjService = subjectsService;
        }
        public bool ShowDateFields { get; set; }

        public string ReturnUrl { get; set; }

        [BindProperty]
        public FinalControl Control { get; set; }
        public bool AddMode { get; set; }
        public async Task<IActionResult> OnGetAsync(int? schId, int? stId, string url)
        {
            ReturnUrl = url;
            AddMode = false;
            if (!schId.HasValue || !stId.HasValue) return NotFound();
            return await BuildPage(schId.Value, stId.Value,url);
        }
        public async Task<IActionResult> BuildPage(int schId,int stId,string url)
        {
            ReturnUrl = url;
            Student = await _stService.GetStudentAsync(stId);
            if (Student == null) return NotFound();
            Schedule = Student.Group.Schedules.FirstOrDefault(e => e.Id == schId);
            if (Schedule == null) return NotFound();
            Control = Student.FinalControls.FirstOrDefault(e => e.SubjectId == Schedule.SubjectId);
            if (Control == null)
            {
                AddMode = true;
                Control = new FinalControl();
                Control.WasModified = true;
                Control.SubjectId = Schedule.SubjectId;
                Control.StudentId = Student.Id;
                Control.Date = DateTime.Now;
            }
            return Page();
        }



        public Student Student { get; set; }
        public Schedule Schedule { get; set; }

        public async Task<IActionResult> OnPostAsync(int ?schId,string url = null)
        {
            if (!schId.HasValue) return NotFound();
            url = url ?? Url.Content("~/");
            Control.Student = await _stService.GetStudentAsync(Control.StudentId);
            Control.Subject = await _subjService.GetSubjectAsync(Control.SubjectId);
            ModelState.Remove("Control.Student");
            ModelState.Remove("Control.Subject");
            if (!ModelState.IsValid)
            {
                return await BuildPage(schId.Value, Control.StudentId,url);
            }
            try
            {
                if (Control.Id == 0)
                    await _stService.AddNewFinalControlToStudentAsync(Control, Control.StudentId);
                else
                {
                    Control.WasModified = true;
                    await _stService.UpdateFinalControlAsync(Control);
                }
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException ex)
            {
                ModelState.AddModelError("DbException", "Произошла ошибка! " + ex.Message);
                return await BuildPage(schId.Value, Control.StudentId,url);
            }
            return Redirect(url);
        }

        public async Task<string> GetRecommendedFinal(int studentId, int schId)
        {
            return "Процент качества выполненных студентом работ = " + (await _teachService.GetRecommendedFinalControlMark(studentId, schId)).ToString("0.00%");
        }
    }

}
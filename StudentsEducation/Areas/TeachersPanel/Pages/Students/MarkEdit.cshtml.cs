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
using StudentsEducation.Web.Areas.Account.Services;

namespace StudentsEducation.Web.Areas.TeachersPanel.Pages.Students
{
    public class MarkEditModel : PageModel
    {
        private readonly IdentityService _service;
        private readonly ITeachersAndScheduleSerivce _teachService;
        private readonly IStudentsService _stService;
        private readonly ISubjectAndWorksService _subjService;
        public MarkEditModel(ISubjectAndWorksService subjectsService,IdentityService service, ITeachersAndScheduleSerivce teacherService, IStudentsService studentsService)
        {
            _service = service;
            _teachService = teacherService;
            _stService = studentsService;
            _subjService = subjectsService;
        }
        public bool ShowDateFields { get; set; }

        public string ReturnUrl { get; set; }

        [BindProperty]
        public Mark Mark { get; set; }
        public bool AddMode { get; set; } 
        public async Task<IActionResult> OnGetAsync(int? workId, int? stId, string url)
        {
            if (!workId.HasValue || !stId.HasValue) return NotFound();
           
            return await BuildPage(workId.Value,stId.Value,url);
        }
        public Student Student { get; set; }
        public Work Work { get; set; }

        public async Task<IActionResult> BuildPage(int workId,int stId,string url)
        {
            ReturnUrl = url;
            AddMode = false;
            ShowDateFields = true;
            Student = await _stService.GetStudentAsync(stId);
            if (Student == null) return NotFound();
            Work = await _subjService.GetWorkAsync(workId);
            if (Work == null) return NotFound();
            Mark = Student.Marks.FirstOrDefault(e => e.WorkId == workId);
            if (Mark == null)
            {
                AddMode = true;
                Mark = new Mark();
                Mark.WasCorrected = true;
                Mark.WorkId = Work.Id;
                Mark.StudentId = Student.Id;
                var group = Student.Group;
                var resmark = group.Students.SelectMany(e => e.Marks).FirstOrDefault(e => e.WorkId == Work.Id);
                if (resmark != null)
                {
                    Mark.DateAdd = resmark.DateAdd;
                    Mark.DateToPass = resmark.DateToPass;
                    ShowDateFields = false;
                }
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string url=null)
        {
            url = url ?? Url.Content("~/");
            Mark.Student = await _stService.GetStudentAsync(Mark.StudentId);
            Mark.Work = await _subjService.GetWorkAsync(Mark.WorkId);
            ModelState.Remove("Mark.Student");
            ModelState.Remove("Mark.Work");
            if(!ModelState.IsValid)
            {
                return await BuildPage(Mark.WorkId, Mark.StudentId, url);
            }
            try
            {
                if (Mark.Id == 0)
                    await _stService.AddNewMarkToStudentAsync(Mark, Mark.StudentId);
                else
                {
                    Mark.WasCorrected = true;
                    await _stService.UpdateMarkAsync(Mark);
                }
            }
            catch(Microsoft.EntityFrameworkCore.DbUpdateException ex)
            {
                ModelState.AddModelError("DbException", "Произошла ошибка! " + ex.Message);
                return await BuildPage(Mark.WorkId, Mark.StudentId, url);
            }

            return Redirect(url);
        }


    }
}
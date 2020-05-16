using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentsEducation.Domain.Entities;
using StudentsEducation.Domain.Interfaces;
using StudentsEducation.Infrastructure.Services;

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


        [BindProperty]
        public Mark Mark { get; set; }
        public bool AddMode { get; set; } = true;
        public string ReturnUrl { get; set; }
        public async Task<IActionResult> OnGetAsync(int ?workId, int ?stId)
        {
            AddMode = false;
            ShowDateFields = true;
            if (!workId.HasValue || !stId.HasValue) return NotFound();
            var student = await _stService.GetStudentAsync(stId.Value);
            if (student == null) return NotFound();
            var work = await _subjService.GetWorkAsync(workId.Value);
            if (work == null) return NotFound();
            Mark = student.Marks.FirstOrDefault(e => e.WorkId == workId.Value);
            if (Mark == null)
            {
                AddMode = true;
                Mark = new Mark();
                Mark.WasCorrected = true;
                Mark.Work = work;
                Mark.Student = student;
                var group = student.Group;
                var resmark = group.Students.SelectMany(e => e.Marks).FirstOrDefault(e=>e.WorkId==work.Id);
                if (resmark != null)
                {
                    Mark.DateAdd = resmark.DateAdd;
                    Mark.DateToPass = resmark.DateToPass;
                    ShowDateFields = false;
                }
            }
            return Page();
        }



        public async Task<IActionResult> OnPostAsync(string returnUrl=null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if(!ModelState.IsValid)
            {
                return Page();
            }
            try
            {
                if (AddMode)
                    await _stService.AddNewMarkToStudentAsync(Mark, Mark.Student.Id);
                else
                    await _stService.UpdateMarkAsync(Mark);
            }
            catch(Microsoft.EntityFrameworkCore.DbUpdateException ex)
            {
                ModelState.AddModelError("DbException", "Произошла ошибка! " + ex.Message);
            }

            return LocalRedirect(returnUrl);
        }


    }
}
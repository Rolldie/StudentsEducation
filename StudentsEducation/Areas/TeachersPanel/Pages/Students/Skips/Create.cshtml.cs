using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentsEducation.Domain.Entities;
using StudentsEducation.Domain.Interfaces;
using StudentsEducation.Infrastructure.Data;

namespace StudentsEducation.Web.Areas.TeachersPanel.Pages.Students.Skips
{
    public class CreateModel : PageModel
    {
        private readonly IStudentsService _studService;
        private readonly ISubjectAndWorksService _subjService;

        public CreateModel(IStudentsService studService, ISubjectAndWorksService subjService)
        {
            _studService = studService;
            _subjService = subjService;
        }

        public async Task<IActionResult> OnGetAsync(int? id, int? schId)
        {
            if (!id.HasValue || !schId.HasValue) return NotFound();
            return await InitFields(id.Value,schId.Value);
        }
        public Student Student { get; set; }
        public class SelectElement
        {
            public int ScheduleId { get; set; }
            public string Text { get; set; }
        }
        public Schedule Schedule { get; set; }
        public async Task<IActionResult> InitFields(int id,int schId)
        {
            Skip = new Skip();
            Student = await _studService.GetStudentAsync(id);
            if (Student == null) return NotFound();
            Schedule = await _studService.GetScheduleAsync(schId);
            if (Schedule == null) return NotFound();
            Skip.ScheduleId = Schedule.Id;
            Skip.StudentId = id;
            Skip.Date = DateTime.Now;
            return Page();
        }


        [BindProperty]
        public Skip Skip { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            Skip.Schedule = await _studService.GetScheduleAsync(Skip.ScheduleId);
            ModelState.Remove("Skip.Schedule");
            ModelState.Remove("Skip.Student");
            if (!ModelState.IsValid)
            {
                return await InitFields(Skip.StudentId,Skip.ScheduleId);
            }
            try
            {
                await _studService.AddNewSkipToStudentAsync(Skip, Skip.StudentId);
            }
            catch (DbUpdateException ex)
            {
                ModelState.AddModelError("DbException", $"Произошла ошибка! {ex.Message}");
                return await InitFields(Skip.StudentId,Skip.ScheduleId);
            }
            return RedirectToPage("./Index", new { studId = Skip.StudentId, schId=Skip.ScheduleId });
        }
    }
}

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

namespace StudentsEducation.Web.Areas.Admin.Pages.Students.Skips
{
    public class EditModel : PageModel
    {

        private readonly IStudentsService _studService;
        private readonly ISubjectAndWorksService _subjService;

        public EditModel(IStudentsService studService, ISubjectAndWorksService subjService)
        {
            _studService = studService;
            _subjService = subjService;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (!id.HasValue) return NotFound();
            return await InitFields(id.Value);
        }
        public Student Student { get; set; }
        public class SelectElement
        {
            public int ScheduleId { get; set; }
            public string Text { get; set; }
        }

        public async Task<IActionResult> InitFields(int id)
        {
            Skip = await _studService.GetSkipAssync(id);
            if (Skip == null) return NotFound();
            Student = Skip.Student;
            ViewData["ScheduleId"] = new SelectList((await _studService.GetSchedulesByStudentAsync(Student.Id)).Select(e =>
            new SelectElement { ScheduleId = e.Id, Text = $"{e.Subject.Name} {e.StartsIn.ToShortDateString()}-{e.EndsIn.ToShortDateString()}" }), "ScheduleId", "Text");
            return Page();
        }


        [BindProperty]
        public Skip Skip { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            Skip.Schedule = await _studService.GetScheduleAsync(Skip.ScheduleId);
            Skip.Student = await _studService.GetStudentAsync(Skip.StudentId);
            ModelState.Remove("Skip.Schedule");
            ModelState.Remove("Skip.Student");
            if (!ModelState.IsValid)
            {
                return await InitFields(Skip.StudentId);
            }
            try
            {
                await _studService.UpdateSkipAsync(Skip);
            }
            catch (DbUpdateException ex)
            {
                ModelState.AddModelError("DbException", $"Произошла ошибка! {ex.Message}");
                return await InitFields(Skip.StudentId);
            }
            return RedirectToPage("./Index", new { id = Skip.StudentId });
        }
    }
}

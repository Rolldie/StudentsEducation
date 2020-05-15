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

namespace StudentsEducation.Web.Areas.Admin.Pages.Students.Marks
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
            public int WorkId { get; set; }
            public string Text { get; set; }
        }

        public async Task<IActionResult> InitFields(int id)
        {
            Mark = await _studService.GetMarkAsync(id);
            if(Mark == null) return NotFound();
            Student = Mark.Student;
            if (Student == null) return NotFound();
            ViewData["WorkId"] = new SelectList((await _subjService.GetWorksByStudentAsync(Student.Id, true)).Select(e =>
             new SelectElement { WorkId = e.Id, Text = $"{e.Subject.Name} {e.Name}" }), "WorkId", "Text");
            return Page();
        }


        [BindProperty]
        public Mark Mark { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            Mark.Work = await _subjService.GetWorkAsync(Mark.WorkId);
            Mark.Student = await _studService.GetStudentAsync(Mark.StudentId);
            ModelState.Remove("Mark.Work");
            ModelState.Remove("Mark.Student");
            if (!ModelState.IsValid)
            {
                return await InitFields(Mark.StudentId);
            }
            try
            {
                await _studService.UpdateMarkAsync(Mark);
            }
            catch (DbUpdateException ex)
            {
                ModelState.AddModelError("DbException", $"Произошла ошибка! {ex.Message}");
                return await InitFields(Mark.Id);
            }
            return RedirectToPage("./Index", new { id = Mark.StudentId });
        }
    }
}

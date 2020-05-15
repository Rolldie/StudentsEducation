using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentsEducation.Domain.Entities;
using StudentsEducation.Domain.Interfaces;

namespace StudentsEducation.Web.Areas.Admin.Pages.Students.Marks
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
            Mark = new Mark();
            Student = await _studService.GetStudentAsync(id);
            if (Student == null) return NotFound();
            Mark.StudentId = id;
            Mark.DateAdd = DateTime.Now;
            ViewData["WorkId"] = new SelectList((await _subjService.GetWorksByStudentAsync(Student.Id,false)).Select(e =>
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
            ModelState.Remove("Mark.Work");
            ModelState.Remove("Mark.Student");
            if (!ModelState.IsValid)
            {
                return await InitFields(Mark.StudentId);
            }
            try
            {
                await _studService.AddNewMarkToStudentAsync(Mark, Mark.StudentId);
            }
            catch (DbUpdateException ex)
            {
                ModelState.AddModelError("DbException", $"Произошла ошибка! {ex.Message}");
                return await InitFields(Mark.StudentId);
            }
            return RedirectToPage("./Index", new { id = Mark.StudentId });
        }
    }
}

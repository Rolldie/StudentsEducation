using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using StudentsEducation.Domain.Entities;
using StudentsEducation.Domain.Interfaces;
using StudentsEducation.Infrastructure.Data;

namespace StudentsEducation.Web.Areas.Admin.Pages.Students.FinalControls
{
    public class CreateModel : PageModel
    {
        private readonly IStudentsService _studService;
        private readonly ISubjectAndWorksService _subjService;

        public CreateModel(IStudentsService studService,ISubjectAndWorksService subjService)
        {
            _studService = studService;
          //  _subjService = subjService;
        }

        public async Task<IActionResult> OnGetAsync(int ?id)
        {
            if (!id.HasValue) return NotFound();
            return await InitFields(id.Value);
        }
        public Student Student { get; set; }
        public async Task<IActionResult> InitFields(int id)
        {
            FinalControl = new FinalControl();
            Student = await _studService.GetStudentAsync(id);
            if (Student == null) return NotFound();
            FinalControl.StudentId = id;
            FinalControl.Date = DateTime.Now;
            ViewData["SubjectId"] = new SelectList(await _studService.GetSubjectsByStudentAsync(Student.Id), "Id", "Name");
            return Page();
        }


        [BindProperty]
        public FinalControl FinalControl { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            FinalControl.Subject = await _subjService.GetSubjectAsync(FinalControl.SubjectId);
            ModelState.Remove("FinalControl.Subject");
            ModelState.Remove("FinalControl.Student");
            if (!ModelState.IsValid)
            {
                return await InitFields(FinalControl.StudentId);
            }
            try
            {
                await _studService.AddNewFinalControlToStudentAsync(FinalControl, FinalControl.StudentId);
            }
            catch(DbUpdateException ex)
            {
                ModelState.AddModelError("DbException", $"Произошла ошибка! {ex.Message}");
                return await InitFields(FinalControl.StudentId);
            }
            return RedirectToPage("./Index",new { id = FinalControl.StudentId });
        }
    }
}

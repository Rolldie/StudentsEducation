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
    public class EditModel : PageModel
    {
        private readonly IStudentsService _studService;
        private readonly ISubjectAndWorksService _subjService;

        public EditModel(IStudentsService studService, ISubjectAndWorksService subjService)
        {
            _studService = studService;
            _subjService = subjService;
        }


        [BindProperty]
        public FinalControl FinalControl { get; set; }
        public Student Student { get; set; }


        public async Task<IActionResult> InitFields(int fcId)
        {
            FinalControl = await _studService.GetFinalControl(fcId);
            Student = FinalControl.Student;
            ViewData["SubjectId"] = new SelectList(await _studService.GetSubjectsByStudentAsync(Student.Id,true), "Id", "Name");
            return Page();
        }


        public async Task<IActionResult> OnGetAsync(int? fcId)
        {
            if (!fcId.HasValue)
            {
                return NotFound();
            }
            return await InitFields(fcId.Value);
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            FinalControl.Subject = await _subjService.GetSubjectAsync(FinalControl.SubjectId);
            FinalControl.Student = await _studService.GetStudentAsync(FinalControl.StudentId);
            ModelState.Remove("FinalControl.Subject");
            ModelState.Remove("FinalControl.Student");
            if (!ModelState.IsValid)
            {
                return await InitFields(FinalControl.Id);
            }

            try
            {
                await _studService.UpdateFinalControlAsync(FinalControl);
            }
            catch (DbUpdateException ex)
            {
                ModelState.AddModelError("DbException", "Произошла ошибка при обновлении! " + ex.Message);
                return await InitFields(FinalControl.Id);
            }

            return RedirectToPage("./Index",new { id = FinalControl.StudentId });
        }
    }
}

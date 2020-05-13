using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentsEducation.Domain.Entities;
using StudentsEducation.Domain.Interfaces;

namespace StudentsEducation.Web.Areas.Admin.Pages.Students
{
    public class EditModel : PageModel
    {
        private readonly IStudentsService _service;
        private readonly ICathedrasAndGroupsService _groupsService;

        public EditModel(IStudentsService service, ICathedrasAndGroupsService groupsService)
        {
            _service = service;
            _groupsService = groupsService;
        }

        [BindProperty]
        public Student Student { get; set; }
        public IEnumerable<Group> Groups { get; set; }
        public SelectList GroupList { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "Группа обязательно должна быть указана!")]
        public string SelectedGroup { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await InitProperties(id.Value);


            if (Student == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task InitProperties(int studentId)
        {
            Groups = await _groupsService.GetGroupsAsync();
            Student = await _service.GetStudentAsync(studentId);
            SelectedGroup = Student.Group.Id.ToString();
            GroupList = new SelectList(Groups, "Id", "Name", SelectedGroup);
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            int id = int.Parse(SelectedGroup);
            if (id > 0)
                Student.Group = await _groupsService.GetGroupAsync(id);
            ModelState.Remove("Student.Group");
            if (!ModelState.IsValid || Student.Group==null)
            {
                if (Student.Group == null)
                    ModelState.AddModelError("Student.Group", "Не была введена группа");
                await InitProperties(Student.Id);
                return Page();
            }
            try
            {
                await _service.UpdateStudentAsync(Student);
            }
            catch(DbUpdateException ex)
            {
                ModelState.AddModelError("DbUpdate", "Ошибка при попытке обновить данные, возможно дублирование уникальных полей!");
                await InitProperties(Student.Id);
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}

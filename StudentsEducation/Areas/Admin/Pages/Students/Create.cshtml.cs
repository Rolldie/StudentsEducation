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
    public class CreateModel : PageModel
    {
        private readonly IStudentsService _service;
        private readonly ICathedrasAndGroupsService _groupsService;

        public CreateModel(IStudentsService service, ICathedrasAndGroupsService groupsService)
        {
            _service = service;
            _groupsService = groupsService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            await InitProperties();
            return Page();
        }

        public async Task InitProperties()
        {
            Groups = await _groupsService.GetGroupsAsync();
            GroupList = new SelectList(Groups, "Id", "Name", SelectedGroup);
        }
        public IEnumerable<Group> Groups { get; set; }
        public SelectList GroupList { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "Группа обязательно должна быть указана!")]
        public string SelectedGroup { get; set; }

        [BindProperty]
        public Student Student { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            int id = int.Parse(SelectedGroup);
            Student.Group = (await _groupsService.GetGroupsAsync()).FirstOrDefault(e => e.Id == id);
            ModelState.Remove("Student.Group");
            if (!ModelState.IsValid || Student.Group==null)
            {
                if (Student.Group == null)
                    ModelState.AddModelError("Student.Group", "Не была введена группа");
                await InitProperties();
                return Page();
            }
            try
            {
                await _service.AddNewStudentAsync(Student);
            }
            catch (DbUpdateException ex)
            {
                ModelState.AddModelError("DbUpdate", "Ошибка при попытке обновить данные, возможно дублирование уникальных полей!");
                await InitProperties();
                return Page();
            }
            return RedirectToPage("./Index");
        }
    }
}

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentsEducation.Domain.Entities
{
    [Display(Name = "Преподаватель")]
    public class Teacher:BaseEntity
    {
        [Required(ErrorMessage = "Это поле является необходимым!")]
        [StringLength(100)]
        [Display(Name = "ФИО преподавателя")]
        public string Name { get; set; }

        public virtual IEnumerable<Schedule> Schedules { get; set; }

    }
}

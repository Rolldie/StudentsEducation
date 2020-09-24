using System;
using System.ComponentModel.DataAnnotations;

namespace StudentsEducation.Domain.Entities
{
    [Display(Name = "Итоговый контроль")]
    public class FinalControl : BaseEntity
    {
        [Required(ErrorMessage = "Это поле является необходимым!")]
        [Range(-1,9999)]
        [Display(Name = "Оценка")]
        public double MarkValue { get; set; }

        [Required(ErrorMessage = "Это поле является необходимым!")]
        [DataType(DataType.Date)]
        [Display(Name = "Дата")]
        public DateTime Date { get; set; }

        [Display(Name ="Было изменено")]
        public bool WasModified { get; set; }


        [Required(ErrorMessage = "Это поле является необходимым!")]
        [Display(Name = "Расписание")]
        public int ScheduleId { get; set; }
        [Required(ErrorMessage = "Это поле является необходимым!")]
        public virtual Schedule Schedule { get; set; }


        [Required(ErrorMessage = "Это поле является необходимым!")]
        [Display(Name = "Студент")]
        public int StudentId { get; set; }
        [Required(ErrorMessage = "Это поле является необходимым!")]
        public virtual Student Student { get; set; }
    }
}

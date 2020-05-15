using StudentsEducation.Domain.Validators;
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

        //references

        [Required(ErrorMessage = "Это поле является необходимым!")]
        [Display(Name = "Предмет")]
        public int SubjectId { get; set; }
        [Required(ErrorMessage = "Это поле является необходимым!")]
        public virtual Subject Subject { get; set; }
        [Required(ErrorMessage = "Это поле является необходимым!")]
        [Display(Name = "Студент")]
        public int StudentId { get; set; }
        [Required(ErrorMessage = "Это поле является необходимым!")]
        public virtual Student Student { get; set; }

    }
}

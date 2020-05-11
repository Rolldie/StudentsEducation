using StudentsEducation.Domain.Validators;
using System;
using System.ComponentModel.DataAnnotations;

namespace StudentsEducation.Domain.Entities
{
    [Display(Name = "Итоговый контроль")]
    public class FinalControl : BaseEntity
    {

        [Required]
        [Range(-1,9999)]
        [MarkValidation("Subject",ErrorMessage ="Оценка должна соответствовать типу контроля, выставленного для этого предмета!")]
        [Display(Name = "Оценка")]
        public double MarkValue { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Дата")]
        public DateTime Date { get; set; }

        public bool WasModified { get; set; }

        //references

        [Required]
        public virtual Subject Subject { get; set; }
        [Required]
        public virtual Student Student { get; set; }

    }
}

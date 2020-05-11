using StudentsEducation.Domain.Validators;
using System;
using System.ComponentModel.DataAnnotations;

namespace StudentsEducation.Domain.Entities
{
    [Display(Name = "Оценка")]
    public class Marks : BaseEntity
    {
        [Required]
        [Range(-1,1000)]
        [MarkValidation("Work",ErrorMessage ="Оценка должна соответствовать типу контроля, выбранном для этой работы!")]
        [Display(Name = "Оценка")]
        public double MarkValue { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Дата")]
        public DateTime Date { get; set; } = DateTime.Now;

        [Required]
        [System.ComponentModel.DisplayName("Редактировано учителем")]
        public bool WasModified { get; set; }




        //references

        [Required]
        public virtual Student Student { get; set; }
        [Required]
        public virtual Work Work { get; set; }
    }

}

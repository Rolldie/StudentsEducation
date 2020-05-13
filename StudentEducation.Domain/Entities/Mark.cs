using StudentsEducation.Domain.Validators;
using System;
using System.ComponentModel.DataAnnotations;

namespace StudentsEducation.Domain.Entities
{
    [Display(Name = "Оценка")]
    public class Mark : BaseEntity
    {
        [Required]
        [Range(-1,1000)]
        [MarkValidation("Work",ErrorMessage ="Оценка должна соответствовать типу контроля, выбранному для этой работы!")]
        [Display(Name = "Оценка")]
        public double MarkValue { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Дата задания")]
        public DateTime DateAdd { get; set; } = DateTime.Now;
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Дата до снижения оценки")]
        public DateTime DateToPass { get; set; } = DateTime.Now.AddDays(20);
        [Required]
        [Display(Name="Редактировано учителем")]
        public bool WasModified { get; set; }




        //references
        [Required]
        public int StudentId { get; set; }
        [Required]
        public virtual Student Student { get; set; }
        [Required]
        public int WorkId { get; set; }
        [Required]
        public virtual Work Work { get; set; }
    }

}

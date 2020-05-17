using FoolProof.Core;
using System;
using System.ComponentModel.DataAnnotations;

namespace StudentsEducation.Domain.Entities
{
    [Display(Name = "Оценка")]
    public class Mark : BaseEntity
    {
        [Required(ErrorMessage ="Это поле является необходимым!")]
        [Range(-1,1000)]
        [Display(Name = "Оценка")]
        public double MarkValue { get; set; }
        [Required(ErrorMessage = "Это поле является необходимым!")]
        [DataType(DataType.Date)]
        [Display(Name = "Дата задания")]
        public DateTime DateAdd { get; set; } = DateTime.Now;
        [Required(ErrorMessage = "Это поле является необходимым!")]
        [DataType(DataType.Date)]
        [Display(Name = "Дата до снижения оценки")]
        [GreaterThan("DateAdd",ErrorMessage ="Дата должна быть больше чем дата задания!")]
        public DateTime DateToPass { get; set; } = DateTime.Now.AddDays(20);
        [Required(ErrorMessage = "Это поле является необходимым!")]
        [Display(Name="Редактировано учителем")]
        public bool WasCorrected { get; set; }




        //references
        [Required(ErrorMessage = "Номер стунеднта является необходимым!")]
        public int StudentId { get; set; }
        [Required(ErrorMessage = "Поле студента является необходимым!")]
        public virtual Student Student { get; set; }
        [Required(ErrorMessage = "Номер работы является необходимым!")]
        [Display(Name ="Работа")]
        public int WorkId { get; set; }
        [Required(ErrorMessage = "Поле работы является необходимым!")]
        public virtual Work Work { get; set; }
    }

}

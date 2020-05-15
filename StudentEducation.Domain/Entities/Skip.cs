using System;
using System.ComponentModel.DataAnnotations;

namespace StudentsEducation.Domain.Entities
{
    [Display(Name = "Пропуск")]
    public class Skip :BaseEntity
    {
        [Required(ErrorMessage = "Это поле является необходимым!")]
        [StringLength(500)]
        [Display(Name = "Пояснение к пропуску")]
        public string Information { get; set; }
        [Required(ErrorMessage = "Это поле является необходимым!")]
        [Display(Name = "Уважительный")]
        public bool IsGood { get; set; }

        [Required(ErrorMessage = "Это поле является необходимым!")]
        [DataType(DataType.Date)]
        [Display(Name ="День пропуска")]
        public DateTime Date { get; set; }

        //references
        public int StudentId { get; set; }
        public virtual Student Student { get; set; }
        [Required(ErrorMessage = "Это поле является необходимым!")]
        public int ScheduleId { get; set; }
        [Required(ErrorMessage = "Это поле является необходимым!")]
        public virtual Schedule Schedule { get; set; }
    }
}

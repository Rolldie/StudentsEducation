using System.ComponentModel.DataAnnotations;

namespace StudentsEducation.Domain.Entities
{
    [Display(Name = "Пропуск")]
    public class Skip :BaseEntity
    {
        [Required]
        [StringLength(500)]
        [Display(Name = "Пояснение к пропуску")]
        public string Information { get; set; }
        [Required]
        [Display(Name = "Уважительный")]
        public bool IsGood { get; set; } 

        //references
        public virtual Student Student { get; set; }
        [Required]
        public virtual Schedule Schedule { get; set; }
    }
}

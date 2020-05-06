using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

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

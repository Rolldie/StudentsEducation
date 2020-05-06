using StudentsEducation.Domain.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StudentsEducation.Domain.Entities
{
    [Display(Name = "Итоговый контроль")]
    public class FinalControl : BaseEntity
    {

        [Required]
        [Range(-1,9999)]
        [MarkValidation("Subject")]
        [Display(Name = "Оценка")]
        public double MarkValue { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Дата")]
        public DateTime Date { get; set; }


        //references

        [Required]
        public virtual Subject Subject { get; set; }
        [Required]
        public virtual Student Student { get; set; }

    }
}

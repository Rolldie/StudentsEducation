using FoolProof.Core;
using StudentsEducation.Domain.Interfaces;
using StudentsEducation.Domain.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StudentsEducation.Domain.Entities
{
    [Display(Name = "Оценка")]
    public class Marks : BaseEntity
    {
        [Required]
        [Range(-1,1000)]
        [MarkValidation("Work")]
        [Display(Name = "Оценка")]
        public double MarkValue { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Дата")]
        public DateTime Date { get; set; } = DateTime.Now;



        //references

        [Required]
        public virtual Student Student { get; set; }
        [Required]
        public virtual Work Work { get; set; }
    }

}

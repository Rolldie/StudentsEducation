using FoolProof.Core;
using StudentsEducation.Domain.Interfaces;
using StudentsEducation.Domain.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StudentsEducation.Domain.Entities
{
    public class Marks : BaseEntity
    {

        [Required]
        [Range(-1,1000)]
        [MarkValidation("Work")]
        public double MarkValue { get; set; }




        //references

        [Required]
        public virtual Student Student { get; set; }
        [Required]
        public virtual Work Work { get; set; }
    }

}

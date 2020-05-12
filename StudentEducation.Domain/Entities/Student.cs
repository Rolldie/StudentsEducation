using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentsEducation.Domain.Entities
{

    [Display(Name = "Студент")]
    public class Student:BaseEntity
    {
        [Required]
        [StringLength(200)]
        [Display(Name = "ФИО студента")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "День рождения")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        [Required]
        [StringLength(7)]
        [Display(Name = "Номер зачетной книжки")]
        public string GradeBookNumber { get; set; }
        //references
        [Required]
        public virtual Group Group { get; set; }
        public virtual IEnumerable<FinalControl> FinalControls { get; set; }
        public virtual IEnumerable<Mark> Marks { get; set; }
        public virtual IEnumerable<Skip> Skips { get; set; }
    }
}

using FoolProof.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentsEducation.Domain.Entities
{
    [Display(Name = "Группа")]
    public class Group :BaseEntity
    {
        [Required]
        [StringLength(30,ErrorMessage ="Длина строки превышает 30 символов")]
        [Display(Name = "Имя группы")]
        public string Name { get; set; }
        [Required]
        [Range(1,9)]
        [Display(Name = "Номер курса")]
        public int CourseNumber{ get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Начало обучения")]
        public DateTime StartEducationDate { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Конец обучения")]
        [GreaterThan("StartEducationDate",DependentPropertyDisplayName ="Начало обучения",ErrorMessage ="Значение этого поля должно быть больше значения поля `Начало обучения`")]
        public DateTime EndEducationDate{get; set;}


        //references
        [Required]
        public int CathedraId { get; set; }
        [Required]
        public virtual Cathedra Cathedra { get; set; }
        public virtual IEnumerable<Student> Students { get; set; }
        public virtual IEnumerable<Schedule> Schedules { get; set; }
    }
}

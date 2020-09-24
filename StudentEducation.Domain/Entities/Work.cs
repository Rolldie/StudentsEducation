using StudentsEducation.Domain.Interfaces;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentsEducation.Domain.Entities
{
    [Display(Name = "Работа")]
    public class Work:BaseEntity
    {
        [Required]
        [StringLength(200)]
        [Display(Name = "Название работы")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Номер по счету")]
        public int WorkNumber { get; set; }


        [Required]
        public int SubjectId { get; set; }
        [Required]
        public virtual Subject Subject { get; set; }


        public virtual ControlType ControlType { get; set; }


        public virtual IEnumerable<Mark> Marks { get; set; }
        public virtual IEnumerable<WorksSchedule> WorksSchedules { get; set; }

    }
}

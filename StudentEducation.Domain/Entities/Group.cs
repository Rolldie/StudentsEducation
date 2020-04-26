using FoolProof.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StudentsEducation.Domain.Entities
{
    public class Group :BaseEntity
    {
        [Required]
        [StringLength(30)]
        public string Name { get; set; }
        [Required]
        [Range(1,9)]
        public int CourseNumber{ get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime StartEducationDate { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [GreaterThan("StartEducationDate")]
        public DateTime EndEducationDate{get; set;}

        //references
        [Required]
        public virtual int CathedraId { get; set; }
        [Required]
        public virtual Cathedra Cathedra { get; set; }
        public virtual IEnumerable<Student> Students { get; set; }
        public virtual IEnumerable<Schedule> Schedules { get; set; }
    }
}

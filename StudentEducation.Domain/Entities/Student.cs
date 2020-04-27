using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StudentsEducation.Domain.Entities
{
    public class Student:BaseEntity
    {
        [Required]
        [StringLength(200)]
        public string Name { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }


        //references
        [Required]
        public virtual Group Group { get; set; }
        public virtual IEnumerable<FinalControl> FinalControls { get; set; }
        public virtual IEnumerable<Marks> Marks { get; set; }
        public virtual IEnumerable<Skip> Skips { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentsEducation.Domain.Entities
{
    [Display(Name = "Студент")]
    public class Student:BaseEntity
    {
        [Required(ErrorMessage = "Это поле является необходимым!")]
        [StringLength(200)]
        [Display(Name = "ФИО студента")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Это поле является необходимым!")]
        [Display(Name = "День рождения")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "Это поле является необходимым!")]
        [StringLength(6,MinimumLength =6)]
        [RegularExpression(@"(\d{6})",ErrorMessage ="Ошибка, поле не содержит 6 цифр!")]
        [Display(Name = "Номер зачетной книжки")]
        public string GradeBookNumber { get; set; }
        

        [Required]
        public virtual Group Group { get; set; }
        [Required(ErrorMessage = "Это поле является необходимым!")]
        public int GroupId { get; set; }


        public virtual IEnumerable<FinalControl> FinalControls { get; set; }
        public virtual IEnumerable<Mark> Marks { get; set; }
        public virtual IEnumerable<Skip> Skips { get; set; }
    }
}

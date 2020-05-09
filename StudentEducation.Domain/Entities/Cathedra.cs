using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentsEducation.Domain.Entities
{
    [Display(Name = "Кафедра")]
    public class Cathedra:BaseEntity
    {
        [Required]
        [StringLength(100)]
        [Display(Name="Название кафедры")]
        public string Name { get; set; }
        [Required]
        [StringLength(20)]
        [Phone]
        [Display(Name = "Основной номер телефона")]
        public string MainPhoneNumber { get; set; }
        [StringLength(20)]
        [Phone]
        [Display(Name = "Второй номер телефона")]
        public string SecondPhoneNumber { get; set; }
       
        
        
        //references
        public virtual IEnumerable<Group> Groups { get; set; }
    }
}

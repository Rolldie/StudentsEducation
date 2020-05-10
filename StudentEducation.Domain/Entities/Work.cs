using StudentsEducation.Domain.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace StudentsEducation.Domain.Entities
{
    [Display(Name = "Название кафедры")]
    public class Work:BaseEntity, ITypedByControl
    {
        [Required]
        [StringLength(200)]
        [Display(Name = "Название работы")]
        public string Name { get; set; }

        //references
        [Required]
        public virtual Subject Subject { get; set; }
        public virtual ControlType ControlType { get; set; }


        //interface method to use MarkValidation in subentities
        public ControlType GetControlType()
        {
            return ControlType;
        }
    }
}

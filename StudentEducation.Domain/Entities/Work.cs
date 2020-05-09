using StudentsEducation.Domain.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace StudentsEducation.Domain.Entities
{
    [Display(Name = "Название кафедры")]
    public class Work:BaseEntity, ITypedByControl
    {
        [Required]
        [StringLength(200)]
        [Display(Name = "Название кафедры")]
        public string Name { get; set; }

        //references
        [Required]
        public virtual Subject Subject { get; set; }
        [Required]
        public virtual int SubjectId { get; set; }
        public virtual int ControlTypeId { get; set; }
        public virtual ControlType ControlType { get; set; }


        //interface method to use MarkValidation in subentities
        public ControlType GetControlType()
        {
            return ControlType;
        }
    }
}

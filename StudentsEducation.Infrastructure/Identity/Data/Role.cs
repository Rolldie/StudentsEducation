using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
namespace StudentsEducation.Infrastructure.Identity.Data
{
    public class Role: IdentityRole
    {
        [DisplayName("Имя")]
        public override string Name { get => base.Name; set => base.Name = value; }

        [DisplayName("Нормализованое имя")]
        public override string NormalizedName { get => base.NormalizedName; set => base.NormalizedName = value; }
        [DisplayName("Штамп")]
        public override string ConcurrencyStamp { get => base.ConcurrencyStamp; set => base.ConcurrencyStamp = value; }
        [StringLength(255)]
        [DisplayName("Описание")]
        public string Description { get; set; }
        [Required]
        [DisplayName("Имеет связь в БД")]
        public bool IsDatabaseFieldsRequired { get; set; }
    }


}

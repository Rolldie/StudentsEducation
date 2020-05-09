using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
namespace StudentsEducation.Infrastructure.Identity.Data
{
    public class AppUser:IdentityUser
    {
        [DisplayName("Имя пользователя")]
        public override string UserName { get => base.UserName; set => base.UserName = value; }

        [DisplayName("Телефон пользователя")]
        public override string PhoneNumber { get => base.PhoneNumber; set => base.PhoneNumber = value; }
        [DisplayName("Номер соотношения в БД")]
        public string DbId { get; set; }
    }
}

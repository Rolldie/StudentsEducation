using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace StudentsEducation.Infrastructure.Identity.Data
{
    public class AppUser:IdentityUser
    {
        [Required]
        [StringLength(30)]
        [PersonalData]
        public string FirstName { get; set; }
        [Required]
        [StringLength(30)]
        [PersonalData]
        public string MiddleName { get; set; }
        [Required]
        [StringLength(30)]
        [PersonalData]
        public string LastName { get; set; }

        //id for Students and Teachers
        public string DbId { get; set; }
    }
}

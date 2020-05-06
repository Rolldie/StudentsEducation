using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace StudentsEducation.Infrastructure.Identity.Data
{
    public class AppUser:IdentityUser
    {
        public string DbId { get; set; }
    }
}

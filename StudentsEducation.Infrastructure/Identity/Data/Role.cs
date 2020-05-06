using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentsEducation.Infrastructure.Identity.Data
{
    public class Role: IdentityRole
    {
        [System.ComponentModel.DataAnnotations.StringLength(255)]
        public string Description { get; set; }


    }


}

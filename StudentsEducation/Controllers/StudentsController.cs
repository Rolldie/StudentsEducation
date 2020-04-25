using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentsEducation.Domain.Entities;
using StudentsEducation.Domain.Services;
using StudentsEducation.Infrastructure.Data;

namespace StudentsEducation.Web.Controllers
{
    public class StudentsController : Controller
    {
        private readonly StudentService _service;

        public StudentsController(StudentService service)
        {
            _service = service;
        }


        public async Task<IActionResult> Index(int ?id)
        {
            int value = id.GetValueOrDefault();
            if (value > 0)
                return View(_service.GetStudentsByGroup(value));
            else
                return View(_service.GetStudentWithActivity());
        }


    }
}

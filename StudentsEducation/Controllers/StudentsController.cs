using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentsEducation.Domain.Entities;
using StudentsEducation.Infrastructure.Services;
using StudentsEducation.Infrastructure.Data;

namespace StudentsEducation.Web.Controllers
{
    public class StudentsController : Controller
    {
        private readonly StudentsService _service;

        public StudentsController(StudentsService service)
        {
            _service = service;
        }

        //TODO: Сделать асинхронным...
        public async Task<IActionResult> Index(int ?id)
        {
            int value = id.GetValueOrDefault();
            if (value > 0)
                return View(await _service.GetStudentsByGroupAsync(value));
            else
                return View(await _service.GetStudentsAsync());
        }


    }
}

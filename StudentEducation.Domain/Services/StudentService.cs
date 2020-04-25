using StudentsEducation.Domain.Entities;
using StudentsEducation.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StudentsEducation.Domain.Services
{
    public class StudentService
    {
        private readonly IAsyncRepository<Student> _context;
        public StudentService(IAsyncRepository<Student> studentContext)
        {
            _context = studentContext;
        }

        public IEnumerable<Student> GetStudentWithActivity()
        {
            return _context.Get(null,null, "Group,Cathedra");
        }
        public IEnumerable<Student> GetStudentsByGroup(int id)
        {
            return _context.Get(e => e.Group.Id == id, null, includeProperties:"Group");
        }


    }
}

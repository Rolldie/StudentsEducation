using StudentsEducation.Domain.Entities;
using StudentsEducation.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsEducation.Domain.Services
{
    public class StudentsService
    {
        private readonly IAsyncRepository<Student> _context;
        public StudentsService(IAsyncRepository<Student> studentContext)
        {
            _context = studentContext;
        }

        public  async Task<IEnumerable<Student>> GetStudentsAsync()=>await _context.GetAsync(null,null);
       
        public async Task<IEnumerable<Student>> GetStudentsByGroupAsync(int id)=> await _context.GetAsync(e => e.Group.Id == id, null);


    }
}

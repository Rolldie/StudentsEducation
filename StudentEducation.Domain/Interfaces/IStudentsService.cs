using StudentsEducation.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentsEducation.Domain.Interfaces
{
    public interface IStudentsService
    {
        public Task<IEnumerable<Student>> GetStudentsAsync();

        public Task<IEnumerable<Student>> GetStudentsByGroupAsync(int id);

        public Task<Student> AddNewStudentAsync(Student student);
        public Task DeleteStudentAsync(Student student);
        //maybe more
    }
}

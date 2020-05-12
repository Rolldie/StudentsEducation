using StudentsEducation.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentsEducation.Domain.Interfaces
{
    public interface IStudentsService
    {
        public Task<IEnumerable<Student>> GetStudentsAsync();
        public Task<IEnumerable<Mark>> GetMarksAsynс();
        public Task<IEnumerable<Skip>> GetSkipsAsync();
        public Task<IEnumerable<FinalControl>> GetFinalControlsAsync();
        public Task<Student> GetStudentAsync(int id);
        public Task<Mark> GetMarkAsync(int id);
        public Task<Skip> GetSkipAssync(int id);
        public Task<FinalControl> GetFinalControl(int id);
        public Task<IEnumerable<Mark>> GetMarksByStudentAsync(int studentId);
        public Task<IEnumerable<Skip>> GetSkipsByStudentAsync(int studentId);
        public Task<IEnumerable<FinalControl>> GetFinalControlsByStudentAsync(int studentId);
        public Task<IEnumerable<Student>> GetStudentsByGroupAsync(int id);
        public Task<Student> AddNewStudentAsync(Student student);
        public Task UpdateStudentAsync(Student student);
        public Task DeleteStudentAsync(int id);
        public Task<Mark> AddNewMarkToStudentAsync(Mark mark,int studentId);
        public Task UpdateMarkAsync(Mark mark);
        public Task DeleteMarkAsync(int id);
        public Task<FinalControl> AddNewFinalControlToStudentAsync(FinalControl finalControl,int studentId);
        public Task UpdateFinalControlAsync(FinalControl finalControl);
        public Task DeleteFinalControlAsync(int id);
        //maybe more
    }
}

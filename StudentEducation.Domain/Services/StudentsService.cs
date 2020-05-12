using StudentsEducation.Domain.Entities;
using StudentsEducation.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentsEducation.Domain.Services
{
    public class StudentsService:IStudentsService
    {
        private readonly IAsyncRepository<Student> _studContext;
        private readonly IAsyncRepository<Mark> _markContext;
        private readonly IAsyncRepository<FinalControl> _finalControlContext;
        private readonly IAsyncRepository<Skip> _skipsContext;

        public StudentsService(IAsyncRepository<Student> studContext, IAsyncRepository<Mark> markContext, IAsyncRepository<FinalControl> finalControlContext, IAsyncRepository<Skip> skipsContext)
        {
            _studContext = studContext;
            _markContext = markContext;
            _finalControlContext = finalControlContext;
            _skipsContext = skipsContext;
        }

        public  async Task<IEnumerable<Student>> GetStudentsAsync()=>await _studContext.GetAsync(null,null);
       
        public async Task<IEnumerable<Student>> GetStudentsByGroupAsync(int id) =>
            await _studContext.GetAsync(e => e.Group.Id == id, null);

        public async Task<Student> AddNewStudentAsync(Student student)
        {
            return await _studContext.CreateAsync(student);
        }

        public async Task DeleteStudentAsync(Student student)
        {
            await _studContext.DeleteAsync(student.Id);
        }

        public async Task<IEnumerable<Mark>> GetMarksAsynс()
        {
            return await _markContext.GetAllAsync();
        }

        public async Task<IEnumerable<Skip>> GetSkipsAsync()
        {
            return await _skipsContext.GetAllAsync();
        }

        public async Task<IEnumerable<FinalControl>> GetFinalControlsAsync()
        {
            return await _finalControlContext.GetAllAsync();
        }

        public async Task<Student> GetStudentAsync(int id)
        {
            return await _studContext.GetByIdAsync(id);
        }

        public async Task<Mark> GetMarkAsync(int id)
        {
            return await _markContext.GetByIdAsync(id);
        }

        public async Task<Skip> GetSkipAssync(int id)
        {
            return await _skipsContext.GetByIdAsync(id);
        }

        public async Task<FinalControl> GetFinalControl(int id)
        {
            return await _finalControlContext.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Mark>> GetMarksByStudentAsync(int studentId)
        {
            var student = await _studContext.GetByIdAsync(studentId);
            if (student == null) return null;
            else
            {
                return student.Marks.AsEnumerable();
            }

        }

        public async Task<IEnumerable<Skip>> GetSkipsByStudentAsync(int studentId)
        {
            var student = await _studContext.GetByIdAsync(studentId);
            if (student == null) return null;
            else
            {
                return student.Skips.AsEnumerable();
            }
        }

        public async  Task<IEnumerable<FinalControl>> GetFinalControlsByStudentAsync(int studentId)
        {
            var student = await _studContext.GetByIdAsync(studentId);
            if (student == null) return null;
            else
            {
                return student.FinalControls.AsEnumerable();
            }
        }

        public async Task UpdateStudentAsync(Student student)
        {
            await _studContext.UpdateAsync(student);
        }

        public async  Task DeleteStudentAsync(int id)
        {
            var student = await _studContext.GetByIdAsync(id);
            if (student != null) await _studContext.DeleteAsync(student.Id);
        }

        public async Task<Mark> AddNewMarkToStudentAsync(Mark mark,int studentId)
        {
            var student = await _studContext.GetByIdAsync(studentId);
            if(student!=null)
            {
                mark.Student = student;
                return await _markContext.CreateAsync(mark);    
            }
            return null;
        }

        public async Task UpdateMarkAsync(Mark mark)
        {
            await _markContext.UpdateAsync(mark);
        }

        public async Task DeleteMarkAsync(int id)
        {
            var mark = await _markContext.GetByIdAsync(id);
            if (mark != null) await _markContext.DeleteAsync(id);
        }

        public async Task<FinalControl> AddNewFinalControlToStudentAsync(FinalControl finalControl,int studentId)
        {
            var student = await _studContext.GetByIdAsync(studentId);
            if (student != null)
            {
                finalControl.Student = student;
                return await _finalControlContext.CreateAsync(finalControl);
            }
            return null;
        }

        public async Task UpdateFinalControlAsync(FinalControl finalControl)
        {
            await _finalControlContext.UpdateAsync(finalControl);
        }

        public async Task DeleteFinalControlAsync(int id)
        {
            var cntrl = await _finalControlContext.GetByIdAsync(id);
            if (cntrl != null) await _finalControlContext.DeleteAsync(id);
        }
    }
}

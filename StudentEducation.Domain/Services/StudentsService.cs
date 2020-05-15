using Microsoft.EntityFrameworkCore;
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
        private readonly IAsyncRepository<Subject> _subjContext;
        private readonly IAsyncRepository<Schedule> _schContext;
        public StudentsService(IAsyncRepository<Student> studContext, 
            IAsyncRepository<Mark> markContext, 
            IAsyncRepository<FinalControl> finalControlContext, 
            IAsyncRepository<Skip> skipsContext,
            IAsyncRepository<Subject> subjectContext,
            IAsyncRepository<Schedule> scheduleContext)
        {
            _studContext = studContext;
            _markContext = markContext;
            _finalControlContext = finalControlContext;
            _skipsContext = skipsContext;
            _subjContext = subjectContext;
            _schContext = scheduleContext;
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
                var controlType = mark.Work.ControlType;
                if (controlType.LowValue >= mark.MarkValue && controlType.HighValue <= mark.MarkValue)
                {
                    var schedule = mark.Student.Group.Schedules.Where(e => e.SubjectId == mark.Work.SubjectId).FirstOrDefault();
                    if (schedule.StartsIn >= mark.DateAdd && schedule.EndsIn <= mark.DateToPass)
                    {
                        try
                        {
                            return await _markContext.CreateAsync(mark);
                        }
                        catch(DbUpdateException ex)
                        {
                            throw ex;
                        }
                    }
                    else throw new DbUpdateException("Промежуток приема работы выходит за рамки промежутка преподавания этого предмета для этого студенты (группы)!");
                } 
                else throw new DbUpdateException("Оценка выходит за границы заданого типа контроля!");
            }
            return null;
        }

        public async Task UpdateMarkAsync(Mark mark)
        {
            var student = mark.Student;
            if (student != null)
            {
                mark.Student = student;
                var controlType = mark.Work.ControlType;
                if (controlType.LowValue <= mark.MarkValue && controlType.HighValue >= mark.MarkValue)
                {
                    var schedule = mark.Student.Group.Schedules.Where(e => e.SubjectId == mark.Work.SubjectId).FirstOrDefault();
                    if (schedule.StartsIn <= mark.DateAdd && schedule.EndsIn >=mark.DateToPass)
                    {
                        try
                        {
                            await _markContext.UpdateAsync(mark);
                        }
                        catch (DbUpdateException ex)
                        {
                            throw ex;
                        }
                    }
                    else throw new DbUpdateException("Промежуток приема работы выходит за рамки промежутка преподавания этого предмета для этого студенты (группы)!");
                }
                else throw new DbUpdateException("Оценка выходит за границы заданого типа контроля!");
            }
            else throw new DbUpdateException("Не удалось найти студента!");
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
                if (ValidateMark(finalControl.MarkValue, finalControl.Subject.ControlType))
                {
                    if (student.Group.Schedules.Any(e => e.SubjectId == finalControl.SubjectId))
                        return await _finalControlContext.CreateAsync(finalControl);
                    else throw new DbUpdateException("Такого предмета не было у этого студента!");
                }
                else throw new DbUpdateException("Данная оценка не соответствует выбранному типу контроля!");
            }
            return null;
        }

        private bool ValidateMark(double value,ControlType type)
        {
            if (value >= type.LowValue && value <= type.HighValue)
                return true;
            else
                return false;
        }

        public async Task UpdateFinalControlAsync(FinalControl finalControl)
        {
            await _finalControlContext.UpdateAsync(finalControl);
            var student = finalControl.Student;
            if (student != null)
            {
                finalControl.Student = student;
                if (ValidateMark(finalControl.MarkValue, finalControl.Subject.ControlType))
                {
                    if (student.Group.Schedules.Any(e => e.SubjectId == finalControl.SubjectId))
                        await _finalControlContext.UpdateAsync(finalControl);
                    else throw new DbUpdateException("Такого предмета не было у этого студента!");
                }
                else throw new DbUpdateException("Данная оценка не соответствует выбранному типу контроля!");
            }
            else throw new DbUpdateException("Не найден пользователь!");
        }

        public async Task DeleteFinalControlAsync(int id)
        {
            var cntrl = await _finalControlContext.GetByIdAsync(id);
            if (cntrl != null) await _finalControlContext.DeleteAsync(id);
        }
        public async Task<IEnumerable<Subject>> GetSubjectsByStudentAsync(int studentId)
        {
            var student = await _studContext.GetByIdAsync(studentId);
            if (student != null)
            {
                var subjects = (await _subjContext.GetAllAsync()).Where(e => e.Schedules.Any(e => e.GroupId == student.GroupId));
                return subjects;
            }
            else return null;
        }
        public async Task<IEnumerable<Schedule>> GetSchedulesByStudentAsync(int studentId)
        {
            var student = await _studContext.GetByIdAsync(studentId);
            if (student != null)
            {
                var schedules = student.Group.Schedules;
                return schedules;
            }
            else return null;
        }
        public async Task<Schedule> GetScheduleAsync(int id)
        {
            return await _schContext.GetByIdAsync(id);
        }

        public async Task<Skip> AddNewSkipToStudentAsync(Skip skip, int studentId)
        {
            var student = await _studContext.GetByIdAsync(studentId);
            if(student!=null)
            {
                skip.Student = student;
                if (skip.Date >= skip.Schedule.StartsIn && skip.Date <= skip.Schedule.EndsIn)
                {
                    var skips = await _skipsContext.GetAllAsync();
                    if (skips.Where(e => e.StudentId == student.Id && e.Date==skip.Date).Count() >= 4) 
                        throw new DbUpdateException("Пропусков не может быть больше 4 за день!");
                    else return await _skipsContext.CreateAsync(skip);
                }
                else throw new DbUpdateException("Пропуск не подходит по дате для этого рассписания!");
            }
            else return null;
        }

        public async Task UpdateSkipAsync(Skip skip)
        {
            var student = await _studContext.GetByIdAsync(skip.StudentId);
            if (student != null)
            {
                if (skip.Date >= skip.Schedule.StartsIn && skip.Date <= skip.Schedule.EndsIn)
                {
                    var skips =
                        await _skipsContext.GetDetachedAllAsync();
                    if (skips.Where(e => e.StudentId == student.Id && e.Date == skip.Date && e.Id !=skip.Id).Count() >= 4)
                        throw new DbUpdateException("Пропусков не может быть больше 4 за день!");
                    else
                    {
                        await _skipsContext.UpdateAsync(skip);
                    }
                }
                else throw new DbUpdateException("Пропуск не подходит по дате для этого рассписания!");
            }
        }

        public Task DeleteSkipAsync(int id)
        {
            throw new System.NotImplementedException();
        }

    }
}

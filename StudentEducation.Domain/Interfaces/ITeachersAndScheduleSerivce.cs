using StudentsEducation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StudentsEducation.Domain.Interfaces
{
    public interface ITeachersAndScheduleSerivce
    {
        public Task<Teacher> GetTeacherAsync(int id);
        public Task<Schedule> GetScheduleAsync(int id);
        public Task<IEnumerable<Schedule>> GetSchedulesByTeacherAsync(int id);
        public Task UpdateTeacherAsync(Teacher teacher);
        public Task UpdateScheduleAsync(Schedule schedule);
        public Task DeleteTeacherAsync(int id);
        public Task DeleteScheduleAsync(int id);

    }
}

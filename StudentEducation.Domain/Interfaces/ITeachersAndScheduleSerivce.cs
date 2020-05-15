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
        public Task<Subject> GetSubjectAsync(int id);
        public Task<Schedule> GetScheduleAsync(int id);
        public Task<Group> GetGroupAsync(int id);
        public Task<IEnumerable<Teacher>> GetTeachersAsync();
        public Task<IEnumerable<Group>> GetGroupsAsync();
        public Task<IEnumerable<Subject>> GetSubjectsAsync();
        public Task<IEnumerable<Schedule>> GetSchedulesByTeacherAsync(int id);
        public Task AddScheduleByTeacherNGroupNSubjectAsync(Schedule schedule,int teacherId, int groupId, int subjectId);
        public Task UpdateTeacherAsync(Teacher teacher);
        public Task UpdateScheduleAsync(Schedule schedule);
        public Task DeleteTeacherAsync(int id);
        public Task DeleteScheduleAsync(int id);

        public Task<IEnumerable<Group>> GetTeachersGroups(int teacherId);

    }
}

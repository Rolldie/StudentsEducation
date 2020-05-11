using StudentsEducation.Domain.Entities;
using StudentsEducation.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace StudentsEducation.Domain.Services
{
    public class TeacherManageService : ITeachersAndScheduleSerivce
    {
        private readonly IAsyncRepository<Teacher> _teacherRepository;
        private readonly IAsyncRepository<Group> _groupRepository;
        private readonly IAsyncRepository<Schedule> _scheduleRepository;
        private readonly IAsyncRepository<Subject> _subjectRepository;
        public TeacherManageService(IAsyncRepository<Group> groupRepository,
            IAsyncRepository<Teacher> teacherRepository,
            IAsyncRepository<Schedule> scheduleRepository,IAsyncRepository<Subject> subjectRepository)
        {
            _groupRepository = groupRepository;
            _teacherRepository = teacherRepository;
            _scheduleRepository = scheduleRepository;
            _subjectRepository = subjectRepository;
        }
        public async Task DeleteScheduleAsync(int id)
        {
            await _scheduleRepository.DeleteAsync(id);
        }

        public async Task DeleteTeacherAsync(int id)
        {
            await _teacherRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Group>> GetGroupsAsync()
        {
            return await _groupRepository.GetAllAsync();
        }

        public async Task<Schedule> GetScheduleAsync(int id)
        {
            return await _scheduleRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Schedule>> GetSchedulesByTeacherAsync(int id)
        {
            return await _scheduleRepository.GetAsync(e => e.Teacher.Id == id);
        }

        public async Task<Teacher> GetTeacherAsync(int id)
        {
            return await _teacherRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Teacher>> GetTeachersAsync()
        {
            return await _teacherRepository.GetAllAsync();
        }

        public async Task UpdateScheduleAsync(Schedule schedule)
        {
            await _scheduleRepository.UpdateAsync(schedule);
        }

        public async Task UpdateTeacherAsync(Teacher teacher)
        {
            await _teacherRepository.UpdateAsync(teacher);
        } 
        public async Task<IEnumerable<Subject>> GetSubjectsAsync()
        {
            return await _subjectRepository.GetAllAsync();
        }

        public async Task<Subject> GetSubjectAsync(int id)
        {
            return await _subjectRepository.GetByIdAsync(id);
        }

        public async Task<Group> GetGroupAsync(int id)
        {
            return await _groupRepository.GetByIdAsync(id);
        }

        public async Task AddScheduleByTeacherNGroupNSubjectAsync(Schedule schedule, int teacherId, int groupId, int subjectId)
        {
            var teacher = await _teacherRepository.GetByIdAsync(teacherId);
            var group = await _groupRepository.GetByIdAsync(groupId);
            var subject = await _subjectRepository.GetByIdAsync(subjectId);
            if(teacher!=null && group!=null && subject!=null)
            {
                schedule.Teacher = teacher;
                schedule.Subject = subject;
                schedule.Group = group;
                await _scheduleRepository.CreateAsync(schedule);
            }
        }
    }
}

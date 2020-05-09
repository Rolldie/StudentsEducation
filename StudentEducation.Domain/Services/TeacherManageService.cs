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
        private readonly IAsyncRepository<Schedule> _scheduleRepository;

        public TeacherManageService(IAsyncRepository<Teacher> teacherRepository,IAsyncRepository<Schedule> scheduleRepository)
        {
            _teacherRepository = teacherRepository;
            _scheduleRepository = scheduleRepository;
        }
        public async Task DeleteScheduleAsync(int id)
        {
            await _scheduleRepository.DeleteAsync(id);
        }

        public async Task DeleteTeacherAsync(int id)
        {
            await _teacherRepository.DeleteAsync(id);
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

        public async Task UpdateScheduleAsync(Schedule schedule)
        {
            await _scheduleRepository.UpdateAsync(schedule);
        }

        public async Task UpdateTeacherAsync(Teacher teacher)
        {
            await _teacherRepository.UpdateAsync(teacher);
        } 
    }
}

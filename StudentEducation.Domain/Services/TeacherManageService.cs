using Microsoft.EntityFrameworkCore;
using StudentsEducation.Domain.Entities;
using StudentsEducation.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

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
            var teacher = schedule.Teacher;
            var group = schedule.Group; 
            var subject = schedule.Subject;
            if (teacher != null && group != null && subject != null)
            {
                if (!(Between(schedule.StartsIn, group.StartEducationDate, group.EndEducationDate) && Between(schedule.EndsIn, group.StartEducationDate, group.EndEducationDate)))
                    throw new DbUpdateException("Данное расписание не подходит группе, т.к. оно выходит за промежуток обучения группы!");
                var checkSchedules = group.Schedules.Where(e=>e.SubjectId==subject.Id && e.Id!=schedule.Id);
                if (checkSchedules.Select(e => 
                e.TeacherId==teacher.Id
                && (Between(schedule.StartsIn, e.StartsIn, e.EndsIn) || Between(schedule.EndsIn, e.StartsIn, e.EndsIn))).Count() > 0)
                    throw new DbUpdateException("Данные предмет уже читается для этой группы!");
                else
                await _scheduleRepository.UpdateAsync(schedule);
            }
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
            schedule.TeacherId = teacherId;
            schedule.GroupId = groupId;
            schedule.SubjectId = subjectId;
            var group = await _groupRepository.GetByIdAsync(groupId);
            var subject = await _subjectRepository.GetByIdAsync(subjectId);
            if(teacher!=null && group!=null && subject!=null)
            {
                schedule.Teacher = teacher;
                schedule.Subject = subject;
                schedule.Group = group; 
                if (!(Between(schedule.StartsIn, group.StartEducationDate, group.EndEducationDate) && Between(schedule.EndsIn, group.StartEducationDate, group.EndEducationDate)))
                    throw new DbUpdateException("Данное расписание не подходит группе, т.к. оно выходит за промежуток обучения группы!");
                var checkSchedules = group.Schedules.Where(e=>e.SubjectId==schedule.SubjectId);
                if (checkSchedules.Select(e => e.TeacherId==teacher.Id && (Between(schedule.StartsIn, e.StartsIn, e.EndsIn) || Between(schedule.EndsIn, e.StartsIn, e.EndsIn))
                    ).Count() > 0)
                    throw new DbUpdateException("Данные предмет уже был пройден!");
                else
                    await _scheduleRepository.CreateAsync(schedule);
            }
        }
        public bool Between(DateTime input, DateTime date1, DateTime date2)
        {
            return (input >= date1 && input <= date2);
        }

        public async Task<IEnumerable<Group>> GetTeachersGroups(int teacherId)
        {
            var teacher = await _teacherRepository.GetByIdAsync(teacherId);
            if (teacher != null)
            {
                var groups = teacher.Schedules.Select(e => e.Group);
                groups = groups.Distinct();
                return groups;
            }
            else return null;
        }

        public async Task<double> GetRecommendedFinalControlMark(int studentId, int scheduleId)
        {
            return 0.0;
        }
    }
}

using StudentsEducation.Domain.Entities;
using StudentsEducation.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsEducation.Domain.Services
{
    public class SubjectManageService:ISubjectAndWorksService
    {
        private readonly IAsyncRepository<Subject> _subjectRepository;
        private readonly IAsyncRepository<Work> _workRepository;
        private readonly IAsyncRepository<ControlType> _cTypesRepository;
        private readonly IAsyncRepository<Student> _studentRepository;

        public SubjectManageService(IAsyncRepository<Student> studentRepository,IAsyncRepository<Subject> subjectRepository,IAsyncRepository<Work> workRepository, IAsyncRepository<ControlType> cTypesRepository)
        {
            _studentRepository = studentRepository;
            _subjectRepository = subjectRepository;
            _workRepository = workRepository;
            _cTypesRepository = cTypesRepository;
        }

        public async Task<Subject> GetSubjectAsync(int id)
        {
            return await _subjectRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Subject>> GetSubjectsAsync()
        {
            return await _subjectRepository.GetAllAsync();
        }

        public async Task<Work> GetWorkAsync(int id)
        {
            return await _workRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Work>> GetWorksAsync()
        {
            return await _workRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Work>> GetWorksBySubjectAsync(int id)
        {
            var subject = await _subjectRepository.GetByIdAsync(id);
            return subject.Works;
        }
        public async Task UpdateWorkAsync(Work work)
        {
            await _workRepository.UpdateAsync(work);
        }

        public async Task AddWorkToSubjectAsync(Work work, int subjectId)
        {
            var subject = await _subjectRepository.GetByIdAsync(subjectId);
            work.Subject = subject;
            await _workRepository.UpdateAsync(work);
        }

        public async Task UpdateSubjectAsync(Subject subject)
        {
            await _subjectRepository.UpdateAsync(subject);
        }

        public async Task AddNewSubjectAsync(Subject subject)
        {
            await _subjectRepository.CreateAsync(subject);
        }

        public async Task DeleteWorkAsync(int id)
        {
            await _workRepository.DeleteAsync(id);
        }

        public  async Task DeleteSubjectAsync(int id)
        {
            await _subjectRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<ControlType>> GetControlTypesAsync()
        {
            return await _cTypesRepository.GetAllAsync();
        }
        public async Task<ControlType> GetControlTypeAsync(int id)
        {
            return await _cTypesRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Work>> GetWorksByStudentAsync(int id)
        {
            var student = await _studentRepository.GetByIdAsync(id);
            var works=student.Group.Schedules.Select(e => e.Subject.Works);
            var result= works.SelectMany(e => e);
            return result;
        }
    }
}

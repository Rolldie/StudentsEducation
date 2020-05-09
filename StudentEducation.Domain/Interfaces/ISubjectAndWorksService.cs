using StudentsEducation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StudentsEducation.Domain.Interfaces
{
    public interface ISubjectAndWorksService
    {
        public Task<Subject> GetSubjectAsync(int id);
        public Task<Work> GetWorkAsync(int id);
        public Task<IEnumerable<Work>> GetWorksBySubjectAsync(int id);
        public Task<IEnumerable<Subject>> GetSubjectsAsync();
        public Task<IEnumerable<Work>> GetWorksAsync();
        public Task UpdateWorkAsync(Work work);
        public Task UpdateSubjectAsync(Subject subject);
        public Task AddWorkToSubjectAsync(Work work, int subjectId);
        public Task AddNewSubjectAsync(Subject subject);
        public Task DeleteWorkAsync(int id);
        public Task DeleteSubjectAsync(int id);
        public Task<IEnumerable<ControlType>> GetControlTypesAsync();
        public Task<ControlType> GetControlTypeAsync(int id);
        
    }
}

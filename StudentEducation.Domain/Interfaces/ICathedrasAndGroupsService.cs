using StudentsEducation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentsEducation.Domain.Interfaces
{
    public interface ICathedrasAndGroupsService
    {
        public Task<IEnumerable<Cathedra>> GetCathedrasAsync();
        public Task<Cathedra> GetCathedraByIdAsync(int cathedraId);
        public Task<IEnumerable<Group>> GetGroupsAsync();
        public Task<Cathedra> CreateCathedraAsync(Cathedra cathedra);
        public Task DeleteCathedraAsync(int cathedraId);
        public Task UpdateCathedraAsync(Cathedra cathedra);
        public Task AddGroupToCathedraAsync(int cathedraId, Group group);
        public Task<IEnumerable<Group>> GetCatherdaGroupsAsync(int cathedraId);
        public Task<Group> GetGroupAsync(int groupId);
        public Task UpdateGroupAsync(Group group);
        public Task DeleteGroupAsync(int id);
        public Task<double> GetGroupAcademicPerfomanceAsync(int cathedraId,DateTime start, DateTime end);

    }
}

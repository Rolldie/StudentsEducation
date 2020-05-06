using StudentsEducation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StudentsEducation.Domain.Interfaces
{
    public interface ICathedrasAndGroupsService
    {
        public Task<IEnumerable<Cathedra>> GetCathedrasAsync();
        public Task<Cathedra> GetCathedraByIdAsync(int cathedraId);
        public Task<Cathedra> CreateCathedraAsync(Cathedra cathedra);
        public Task DeleteCathedraAsync(int cathedraId);
        public Task UpdateCathedraAsync(Cathedra cathedra);
        public Task AddGroupToCathedraAsync(int cathedraId, Group group);
        public Task<IEnumerable<Group>> GetCatherdaGroupsAsync(int cathedraId);

    }
}

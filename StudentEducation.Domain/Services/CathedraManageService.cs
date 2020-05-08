using StudentsEducation.Domain.Entities;
using StudentsEducation.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StudentsEducation.Domain.Services
{
    public class CathedraManageService : ICathedrasAndGroupsService
    {
        private readonly IAsyncRepository<Cathedra> _cathedraRepository;
        private readonly IAsyncRepository<Group> _groupRepository;
        public CathedraManageService(IAsyncRepository<Cathedra> repository,IAsyncRepository<Group> groupRepository)
        {
            _cathedraRepository = repository;
            _groupRepository = groupRepository;
        }

        public async Task AddGroupToCathedraAsync(int cathedraId, Group group)
        {
            var cathedra = await _cathedraRepository.GetByIdAsync(cathedraId);
            group.Cathedra = cathedra;
            await _groupRepository.CreateAsync(group);
        }

        public async Task<Cathedra> CreateCathedraAsync(Cathedra cathedra)
        {
            return await _cathedraRepository.CreateAsync(cathedra);
        }

        public async Task DeleteCathedraAsync(int cathedraId)
        {
            if((await _cathedraRepository.GetByIdAsync(cathedraId))!=null)
                await _cathedraRepository.DeleteAsync(cathedraId);
        }

        public async Task<Cathedra> GetCathedraByIdAsync(int cathedraId)
        {
            return await _cathedraRepository.GetByIdAsync(cathedraId);
        }

        public async Task<IEnumerable<Cathedra>> GetCathedrasAsync()
        {
            return await _cathedraRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Group>> GetCatherdaGroupsAsync(int cathedraId)
        {
            return (await _cathedraRepository.GetByIdAsync(cathedraId)).Groups;
        }

        public async Task UpdateCathedraAsync(Cathedra cathedra)
        {
            await _cathedraRepository.UpdateAsync(cathedra);
        }
        public async Task UpdateGroupAsync(Group group)
        {
            await _groupRepository.UpdateAsync(group);
        }

        public async Task<Group> GetGroupAsync(int groupId)
        {
            return await _groupRepository.GetByIdAsync(groupId);
        }

        public async Task DeleteGroupAsync(int id)
        {
            await _groupRepository.DeleteAsync(id);
        }
    }
}

using StudentsEducation.Domain.Entities;
using StudentsEducation.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<IEnumerable<Group>> GetGroupsAsync()
        {
            return await _groupRepository.GetAllAsync();
        }

        public async Task<double> GetGroupAcademicPerfomanceAsync(int groupId)
        {
            var  group = await _groupRepository.GetByIdAsync(groupId);           
            if (group == null) return 0;
            double result = 0;
            var schedules = group.Schedules;
            var resultWorks = schedules.Select(e => e.Subject).Select(e => e.Works).SelectMany(e=>e).Distinct();
            var students = group.Students;
            var resmarks = resultWorks.Select(e => e.Marks).SelectMany(e=>e).Distinct();
            resmarks = resmarks.Where(e => e.Student.GroupId == group.Id && e.WasCorrected && e.MarkValue>=e.Work.ControlType.SatisfactorilyValue);

            double allworks = resultWorks.Count();
            double marked = resmarks.Count();

            result = marked / allworks;
            return result;

        }
    }
}

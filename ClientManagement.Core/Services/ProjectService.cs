using ClientManagement.Core.Interfaces;
using ClientManagement.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClientManagement.Core.Services
{
    public class ProjectService: IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }
        public async Task<Project> GetProject(Guid id)
        {
            return await _projectRepository.GetProject(id);
        }
        
        public Task<List<Project>> GetAllProjects(bool isSummary = false)
        {
            return _projectRepository.GetAllProjects(isSummary);
        }
        public async Task<IEnumerable<Employee>> GetEmployeeListForProject(Guid ProjectId)
        {
            var project = await _projectRepository.GetProject(ProjectId);
            return project.Employees;
        }
        public async Task Save(Project project)
        {
            var dbProject = await _projectRepository.GetProject(project.Id);
            if (dbProject == null)
                await _projectRepository.Create(project);
            else
                await _projectRepository.Update(project);
        }

        public async Task Delete(Guid id)
        {
            await _projectRepository.Delete(id);
        }
    }
}

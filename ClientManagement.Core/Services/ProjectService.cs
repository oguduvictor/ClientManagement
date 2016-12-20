using ClientManagement.Core.Data.Repositories;
using ClientManagement.Core.Models;
using System.Collections.Generic;
using System.Linq;
using System;

namespace ClientManagement.Core.Services
{
    public class ProjectService: IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }
        public Project GetProject(int id)
        {
            return _projectRepository.GetProject(id);
        }
        public List<Project> GetAllProjects()
        {
            return _projectRepository.GetAllProjects();
        }
        public List<Employee> GetEmployeeListForProject(int ProjectId)
        {
            var project = _projectRepository.GetProject(ProjectId);
            return project.Employees.ToList();
        }
        public void Save(Project project)
        {
            var dbProject = _projectRepository.GetProject(project.Id);
            if (dbProject == null)
                _projectRepository.Create(project);
            else
                _projectRepository.Update(project);
        }

        public void Delete(int id)
        {
            _projectRepository.Delete(id);
        }
    }
}

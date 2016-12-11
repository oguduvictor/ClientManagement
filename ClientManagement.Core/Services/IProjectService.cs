using ClientManagement.Core.Models;
using System.Collections.Generic;

namespace ClientManagement.Core.Services
{
    public interface IProjectService
    {
        Project GetProject(int id);
        List<Project> GetAllProjects();
        List<Employee> GetEmployeeListForProject(int ProjectId);
        void Save(Project project);
    }
}
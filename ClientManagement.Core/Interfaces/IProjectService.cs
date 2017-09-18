using ClientManagement.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClientManagement.Core.Interfaces
{
    public interface IProjectService
    {
        Task<Project> GetProject(Guid id);
        Task<IEnumerable<Project>> GetAllProjects();
        Task<IEnumerable<Employee>> GetEmployeeListForProject(Guid ProjectId);
        Task Save(Project project);
        Task Delete(Guid id);
    }
}
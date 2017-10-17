using ClientManagement.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClientManagement.Core.Interfaces
{
    public interface IProjectService
    {
        Task<Project> GetProject(Guid id);
        Task<List<Project>> GetAllProjects(bool isSummary = false);
        Task<IEnumerable<Employee>> GetEmployeeListForProject(Guid ProjectId);
        Task Save(Project project);
        Task Delete(Guid id);
    }
}
using ClientManagement.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClientManagement.Core.Interfaces
{
    public interface IProjectRepository
    {
        Task Create(Project project);
        Task<Project> GetProject(Guid id);
        Task<List<Project>> GetAllProjects(bool includeClient = false);
        Task Update(Project project);
        Task Delete(Guid id);
    }
}
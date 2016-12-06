using ClientManagement.Core.Models;
using System.Collections.Generic;

namespace ClientManagement.Core.Data.Repositories
{
    public interface IProjectRepository
    {
        void Create(Project project);
        void Update(Project project);
        List<Project> GetAllProjects();
    }
}
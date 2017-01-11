using ClientManagement.Core.Models;
using System;
using System.Collections.Generic;

namespace ClientManagement.Core.Data.Repositories
{
    public interface IProjectRepository
    {
        void Create(Project project);
        Project GetProject(Guid id);
        List<Project> GetAllProjects();
        void Update(Project project);
        void Delete(Guid id);
    }
}
using ClientManagement.Core.Models;
using System;
using System.Collections.Generic;

namespace ClientManagement.Core.Services
{
    public interface IProjectService
    {
        Project GetProject(Guid id);
        List<Project> GetAllProjects();
        List<Employee> GetEmployeeListForProject(Guid ProjectId);
        void Save(Project project);
        void Delete(Guid id);
    }
}
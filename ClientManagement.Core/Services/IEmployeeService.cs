using ClientManagement.Core.Models;
using System;
using System.Collections.Generic;

namespace ClientManagement.Core.Services
{
    public interface IEmployeeService
    {
        Employee GetEmployee(Guid employeeId);
        List<Employee> GetAllEmployees();
        void Save(Employee employee);
        List<Project> GetProjectListForEmployee(Guid employeeId);
        void Delete(Guid id);
        void AssignProjectToEmployee(Guid employeeId, Guid projectId);
        void RemoveProjectFromEmployee(Guid employeeId, Guid projectId);
    }
}
using ClientManagement.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClientManagement.Core.Interfaces
{
    public interface IEmployeeService
    {
        Task<Employee> GetEmployee(Guid employeeId);
        Task<IEnumerable<Employee>> GetAllEmployees();
        Task Save(Employee employee);
        Task<IEnumerable<Project>> GetProjectListForEmployee(Guid employeeId);
        Task Delete(Guid id);
        Task AssignProjectToEmployee(Guid employeeId, Guid projectId);
        Task RemoveProjectFromEmployee(Guid employeeId, Guid projectId);
    }
}
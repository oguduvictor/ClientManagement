using ClientManagement.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClientManagement.Core.Interfaces
{
    public interface IEmployeeRepository
    {
        Task Create(Employee employee);
        Task<Employee> GetEmployee(Guid id);
        Task<List<Employee>> GetAllEmployees(bool isSummary = false);
        Task Update(Employee employee);
        Task AssignProjectToEmployee(Guid employeeId, Guid projectId);
        Task Delete(Guid id);
    }
}
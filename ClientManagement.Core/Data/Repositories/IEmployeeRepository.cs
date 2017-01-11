using ClientManagement.Core.Models;
using System;
using System.Collections.Generic;

namespace ClientManagement.Core.Data.Repositories
{
    public interface IEmployeeRepository
    {
        void Create(Employee employee);
        Employee GetEmployee(Guid id);
        List<Employee> GetAllEmployees();
        void Update(Employee employee);
        void AssignProjectToEmployee(Guid employeeId, Guid projectId);
        void Delete(Guid id);
    }
}
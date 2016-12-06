using ClientManagement.Core.Models;
using System;
using System.Collections.Generic;

namespace ClientManagement.Core.Data.Repositories
{
    public interface IEmployeeRepository
    {
        Employee GetEmployee(Guid id);

        List<Employee> GetAllEmployees();

        void Create(Employee employee);

        void Update(Employee employee);
    }
}
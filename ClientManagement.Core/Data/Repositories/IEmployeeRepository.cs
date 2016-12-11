using ClientManagement.Core.Models;
using System.Collections.Generic;

namespace ClientManagement.Core.Data.Repositories
{
    public interface IEmployeeRepository
    {
        void Create(Employee employee);
        Employee GetEmployee(int id);
        List<Employee> GetAllEmployees();
        void Update(Employee employee);
    }
}
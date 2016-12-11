using ClientManagement.Core.Models;
using System.Collections.Generic;

namespace ClientManagement.Core.Services
{
    public interface IEmployeeService
    {
        Employee GetEmployee(int employeeId);
        List<Employee> GetAllEmployees();
        void Save(Employee employee);
        List<Project> GetProjectListForEmployee(int employeeId);
        void AssignProjectToEmployee(int employeeId, Project project);
    }
}
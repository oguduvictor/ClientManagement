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
        void Delete(int id);
        void AssignProjectToEmployee(int employeeId, int projectId);
        void ReassignProject(int projectId, int FromEmployeeId, int ToEmployeeId);
        void RemoveProjectFromEmployee(int employeeId, int projectId);
    }
}
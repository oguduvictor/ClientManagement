using ClientManagement.Core.Data.Repositories;
using ClientManagement.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClientManagement.Core.Services
{
    public class EmployeeService: IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        
        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public Employee GetEmployee(int employeeId)
        {
            return _employeeRepository.GetEmployee(employeeId);
        }
        public List<Employee> GetAllEmployees()
        {
            return _employeeRepository.GetAllEmployees().ToList();
        }
        public void Save(Employee employee)
        {
            var dbEmployee = _employeeRepository.GetEmployee(employee.Id);
            employee.Projects = new List<Project>();
            if (dbEmployee == null)
                _employeeRepository.Create(employee);
            else
                _employeeRepository.Update(employee);
        }
        public List<Project> GetProjectListForEmployee(int employeeId)
        {
            var employee = _employeeRepository.GetEmployee(employeeId);
            if (employee == null)
            {
                throw new Exception("Employee does not exist");
            }
            return employee.Projects.ToList();
        }
        public void AssignProjectToEmployee(int employeeId, int projectId)
        {
            _employeeRepository.AssignProjectToEmployee(employeeId, projectId);
        }
        public void DeleteEmployee(int id)
        {
            _employeeRepository.Delete(id);
        }

        public void RemoveProjectFromEmployee(int employeeId, int projectId)
        {
            var employee = GetEmployee(employeeId);
            var project = employee.Projects.FirstOrDefault(x => x.Id == projectId);
            employee.Projects.Remove(project);
        }
        public void ReassignProject(int projectId, int FromEmployeeId, int ToEmployeeId)
        {
            AssignProjectToEmployee(ToEmployeeId, projectId);
            RemoveProjectFromEmployee(FromEmployeeId, projectId);
        }

        public void Delete(int id)
        {
            _employeeRepository.Delete(id);
        }
    }
}
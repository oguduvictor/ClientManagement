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
        public void AssignProjectToEmployee(int employeeId, Project project)
        {
            var employee = GetEmployee(employeeId);
            employee.Projects.Add(project);
        }
    }
}
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
        public Employee GetEmployee(Guid employeeId)
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
        public List<Project> GetProjectListForEmployee(Guid employeeId)
        {
            var employee = _employeeRepository.GetEmployee(employeeId);
            if (employee == null)
            {
                throw new Exception("Employee does not exist");
            }
            return employee.Projects.ToList();
        }
        public void AssignProjectToEmployee(Guid employeeId, Guid projectId)
        {
            _employeeRepository.AssignProjectToEmployee(employeeId, projectId);
        }
        public void DeleteEmployee(Guid id)
        {
            _employeeRepository.Delete(id);
        }

        public void RemoveProjectFromEmployee(Guid employeeId, Guid projectId)
        {
            var employee = GetEmployee(employeeId);
            var project = employee.Projects.FirstOrDefault(x => x.Id == projectId);
            employee.Projects.Remove(project);
        }

        public void Delete(Guid id)
        {
            _employeeRepository.Delete(id);
        }
    }
}
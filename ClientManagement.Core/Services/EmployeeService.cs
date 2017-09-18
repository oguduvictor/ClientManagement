using ClientManagement.Core.Interfaces;
using ClientManagement.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientManagement.Core.Services
{
    public class EmployeeService: IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        
        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public async Task<Employee> GetEmployee(Guid employeeId)
        {
            return await _employeeRepository.GetEmployee(employeeId);
        }
        public async Task<IEnumerable<Employee>> GetAllEmployees()
        {
            return await _employeeRepository.GetAllEmployees();
        }
        public async Task Save(Employee employee)
        {
            var dbEmployee = await _employeeRepository.GetEmployee(employee.Id);
            if (dbEmployee == null)
                await _employeeRepository.Create(employee);
            else
                await _employeeRepository.Update(employee);
        }
        public async Task<IEnumerable<Project>> GetProjectListForEmployee(Guid employeeId)
        {
            var employee = await _employeeRepository.GetEmployee(employeeId);
            if (employee == null)
            {
                throw new Exception("Employee does not exist");
            }
            return employee.Projects;
        }
        public async Task AssignProjectToEmployee(Guid employeeId, Guid projectId)
        {
            await _employeeRepository.AssignProjectToEmployee(employeeId, projectId);
        }
        public async Task DeleteEmployee(Guid id)
        {
            await _employeeRepository.Delete(id);
        }

        public async Task RemoveProjectFromEmployee(Guid employeeId, Guid projectId)
        {
            var employee = await GetEmployee(employeeId);
            var project = employee.Projects.FirstOrDefault(x => x.Id == projectId);
            employee.Projects.Remove(project);
        }

        public async Task Delete(Guid id)
        {
            await _employeeRepository.Delete(id);
        }
    }
}
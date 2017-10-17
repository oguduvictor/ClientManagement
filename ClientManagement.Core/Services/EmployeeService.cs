using ClientManagement.Core.Interfaces;
using ClientManagement.Core.Models;
using ClientManagement.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientManagement.Core.Services
{
    public class EmployeeService: IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IProjectRepository _projectRepository;
        
        public EmployeeService(IEmployeeRepository employeeRepository, IProjectRepository projectRepository)
        {
            _employeeRepository = employeeRepository;
            _projectRepository = projectRepository;
        }
        public async Task<Employee> GetEmployee(Guid employeeId)
        {
            return await _employeeRepository.GetEmployee(employeeId);
        }

        public async Task<IEnumerable<Employee>> GetAllEmployees(bool isSummary = false)
        {
            return await _employeeRepository.GetAllEmployees(isSummary);
        }
        public async Task Save(Employee employee)
        {
            var dbEmployee = await _employeeRepository.GetEmployee(employee.Id);
            if (dbEmployee == null)
                await _employeeRepository.Create(employee);
            else
                await _employeeRepository.Update(employee);
        }

        public async Task<AssignProjectViewModel> GetProjectAssignmentView(Guid employeeId)
        {
            var dbEmployee = await _employeeRepository.GetEmployee(employeeId);
            var projects = await _projectRepository.GetAllProjects();

            return new AssignProjectViewModel
            {
                Employee = dbEmployee.FullName,
                EmployeeId = dbEmployee.Id,
                ProjectIds = projects.Select(x => new KeyValuePair<bool, Guid>())
            };
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
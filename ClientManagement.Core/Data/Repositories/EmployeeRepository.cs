using ClientManagement.Core.Data.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using ClientManagement.Core.Models;

namespace ClientManagement.Core.Data.Repositories
{
    public class EmployeeRepository: IEmployeeRepository, IDisposable
    {
        private readonly DbManagementContext _context;
        private readonly bool _externalContext;
        public EmployeeRepository()
        {
            _context = new DbManagementContext();
        }
        public EmployeeRepository(DbManagementContext context)
        {
            _context = context;
            _externalContext = true;
        }
        public void Create(Employee employee)
        {
            _context.Employees.Add(employee);
            _context.SaveChanges();
        }
        public List<Employee> GetAllEmployees()
        {
            return _context.Employees.ToList();
        }
        public Employee GetEmployee(int employeeId)
        {
            return _context.Employees.Find(employeeId);
        }
        public void Update(Employee employee)
        {
            var dbEmployee = GetEmployee(employee.Id);

            dbEmployee.FirstName = employee.FirstName;
            dbEmployee.LastName = employee.LastName;
            dbEmployee.Gender = employee.Gender;
            dbEmployee.Salary = employee.Salary;
            dbEmployee.SkillLevel = employee.SkillLevel;
                foreach (var project in employee.Projects)
                {
                    if (project.Id == 0)
                    {
                        dbEmployee.Projects.Add(project);
                        continue;
                    }

                    var dbProject = dbEmployee.Projects.FirstOrDefault(x => x.Id == project.Id);

                    if (dbProject != null)
                    {
                        dbProject.Title = project.Title;
                        dbProject.Description = project.Description;
                        dbProject.ProjectStatus = project.ProjectStatus;
                        dbProject.ClientId = dbProject.ClientId;
                    }
                }
            
            _context.SaveChanges();
        }

        public void AssignProjectToEmployee(int employeeId, int projectId)
        {
            var employee = GetEmployee(employeeId);
            var project = _context.Projects.Find(projectId);
            employee.Projects.Add(project);
            _context.SaveChanges();
        }

        public void RemoveProjectFromEmployee(int employeeId, int projectId)
        {
            var employee = _context.Employees.FirstOrDefault(x => x.Id == employeeId);
            var project = _context.Projects.FirstOrDefault(x => x.Id == projectId);
            employee.Projects.Remove(project);
            _context.SaveChanges();
        }
        public void Delete(int id)
        {
            var employee = _context.Employees.Find(id);
            _context.Employees.Remove(employee);
            _context.SaveChanges();
        }

        public void Dispose()
        {
            if (_externalContext || _context == null)
                return;

            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}

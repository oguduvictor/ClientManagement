using ClientManagement.Core.Data.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using ClientManagement.Core.Models;
using System.Data.Entity;

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
            return _context.Employees.Include(x => x.Projects).ToList();
        }
        public Employee GetEmployee(Guid employeeId)
        {
            return GetAllEmployees().FirstOrDefault(x => x.Id == employeeId);
        }
        public void Update(Employee employee)
        {
            var dbEmployee = GetEmployee(employee.Id);

            dbEmployee.FirstName = employee.FirstName;
            dbEmployee.LastName = employee.LastName;
            dbEmployee.Gender = employee.Gender;
            foreach (var project in employee.Projects)
            {
                if (project.Id == null)
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

        public void AssignProjectToEmployee(Guid employeeId, Guid projectId)
        {
            var employee = GetEmployee(employeeId);
            var project = _context.Projects.Find(projectId);
            employee.Projects.Add(project);
            _context.Entry(project).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void RemoveProjectFromEmployee(Guid employeeId, Guid projectId)
        {
            var employee = _context.Employees.FirstOrDefault(x => x.Id == employeeId);
            var project = employee.Projects.FirstOrDefault(x => x.Id == projectId);
            employee.Projects.Remove(project);
            _context.Entry(employee).State = EntityState.Deleted;
            _context.SaveChanges();
        }
        public void Delete(Guid id)
        {
            var employee = GetEmployee(id);
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

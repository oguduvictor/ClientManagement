using ClientManagement.Core.Data.Db;
using ClientManagement.Core.Interfaces;
using ClientManagement.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace ClientManagement.Core.Data.Repositories
{
    public class EmployeeRepository: IEmployeeRepository, IDisposable
    {
        private readonly DbManagementContext _dbContext;
        private readonly bool _externalContext;
        public EmployeeRepository()
        {
            _dbContext = new DbManagementContext();
        }
        public EmployeeRepository(DbManagementContext dbContext)
        {
            _dbContext = dbContext;
            _externalContext = true;
        }
        public async Task Create(Employee employee)
        {
            _dbContext.Employees.Add(employee);
            await _dbContext.SaveChangesAsync();
        }
        public Task<List<Employee>> GetAllEmployees(bool isSummary = false)
        {
            var query = _dbContext.Employees.AsQueryable();

            if (isSummary)
            {
                return query.Select(x => new { x.Id, x.FirstName, x.LastName })
                    .ToListAsync()
                    .ContinueWith(items =>
                    {
                        return items.Result.Select(x => new Employee
                        {
                            Id = x.Id,
                            FirstName = x.FirstName,
                            LastName = x.LastName
                        }).ToList();
                    });
               
            }
            return _dbContext.Employees.Include(x => x.Projects).ToListAsync();
        }
        public async Task<Employee> GetEmployee(Guid employeeId)
        {
            return await _dbContext.Employees.FirstOrDefaultAsync(x => x.Id == employeeId);
        }
        public async Task Update(Employee employee)
        {
            var dbEmployee = await GetEmployee(employee.Id);

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
                    dbProject.Status = project.Status;
                    dbProject.ClientId = dbProject.ClientId;
                }
            }

            _dbContext.SaveChanges();
        }

        public async Task AssignProjectToEmployee(Guid employeeId, Guid projectId)
        {
            var employee = await GetEmployee(employeeId);
            var project = _dbContext.Projects.Find(projectId);
            employee.Projects.Add(project);
            _dbContext.Entry(project).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveProjectFromEmployee(Guid employeeId, Guid projectId)
        {
            var employee = _dbContext.Employees.FirstOrDefault(x => x.Id == employeeId);
            var project = employee.Projects.FirstOrDefault(x => x.Id == projectId);
            employee.Projects.Remove(project);
            _dbContext.Entry(employee).State = EntityState.Deleted;
            await _dbContext.SaveChangesAsync();
        }
        public async Task Delete(Guid id)
        {
            var employee = await GetEmployee(id);
            _dbContext.Employees.Remove(employee);
            _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            if (_externalContext || _dbContext == null)
                return;

            _dbContext.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}

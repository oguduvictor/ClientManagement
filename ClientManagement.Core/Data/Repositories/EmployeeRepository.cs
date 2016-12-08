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

        public void Dispose()
        {
            if (_externalContext || _context == null)
                return;

            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public List<Employee> GetAllEmployees()
        {
            return _context.Employees.ToList();
        }

        public Employee GetEmployee(Guid id)
        {
            return _context.Employees.Find(id);
        }

        public List<Project> GetProjectListForEmployee(Guid id)
        {
            return _context.Projects.Where(x => x.Employees.Equals(id)).ToList();
        }



        public void Update(Employee employee)
        {
            if(!_context.Employees.Local.Contains(employee))
            {
                _context.Employees.Attach(employee);
                _context.Entry(employee).State = EntityState.Modified;
            }
            _context.SaveChanges();
        }
    }
}

using ClientManagement.Core.Data.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientManagement.Core.Models;
using System.Data.Entity;

namespace ClientManagement.Core.Data.Repositories
{
    public class EmployeeRepository: IEmployeeRepository
    {
        private readonly EmployeeManagementContext _context;
        private readonly bool _externalContext;
        public EmployeeRepository()
        {
            _context = new EmployeeManagementContext();
        }
        public EmployeeRepository(EmployeeManagementContext context)
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

        public Employee GetEmployee(Guid id)
        {
            return _context.Employees.Find(id);
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

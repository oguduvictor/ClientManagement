using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientManagement.Core.Models;
using System.Configuration;
using System.Threading;
using System.IO;
using ClientManagement.Core.Data.Db;
using Newtonsoft.Json;
using static Newtonsoft.Json.JsonConvert;

namespace ClientManagement.Core.Data.Repositories
{
    public class FileSystemRepository : IEmployeeRepository
    {
        private readonly string FILE_PATH = ConfigurationManager.AppSettings["ClientEmployeeFilePath"];
        private List<Employee> _employees;
        private static ReaderWriterLockSlim _readerWriterLock = new ReaderWriterLockSlim();
        private List<Project> _projects;

        public void Create(Employee employee)
        {
            var employees = GetAllEmployees();
            
            employees.Add(employee);
            PersistEmployees();
        }

        public List<Employee> GetAllEmployees()
        {
            if (_employees != null)
                return _employees;

            _readerWriterLock.EnterReadLock();

            var employeesJson = default(string);

            try
            {
                employeesJson = File.ReadAllText(FILE_PATH);
            }
            finally
            {
                _readerWriterLock.ExitReadLock();
            }

            _employees = DeserializeObject<List<Employee>>(employeesJson)
                            ?? new List<Employee>();

            return _employees;
        }

        public Employee GetEmployee(Guid id)
        {
            var employees = GetAllEmployees();
            var employee = employees.FirstOrDefault(x => x.Id == x.Id);

            return employee;
        }

        public void Update(Employee employee)
        {
            var employees = GetAllEmployees();
            var employeeEntity = employees.FirstOrDefault(x => x.Id == employee.Id);

            if (employeeEntity == null)
                throw new InvalidOperationException("Unknown employee");

            employeeEntity.FirstName = employee.FirstName;
            employeeEntity.LastName = employee.LastName;
            employeeEntity.Gender = employee.Gender;

            PersistEmployees();
        }

        private void PersistEmployees()
        {
            List<Employee> employees = GetAllEmployees();
            var employeesJson = SerializeObject(employees, Formatting.Indented);

            _readerWriterLock.EnterWriteLock();

            try
            {
                File.WriteAllText(FILE_PATH, employeesJson);
            }
            finally
            {
                _readerWriterLock.ExitWriteLock();
            }
        }

        public void AssignProjectToEmployee(Guid employeeId, Guid projectId)
        {
            var Projects = GetAllProjects();
            var project = Projects.FirstOrDefault(x => x.Id == projectId);
            var employee = GetEmployee(employeeId);

            employee.Projects.Add(project);

            PersistEmployees();
        }

        public List<Project> GetAllProjects()
        {
            if (_projects != null)
                return _projects;

            _readerWriterLock.EnterReadLock();
            var projectjson = default(string);

            try
            {
                projectjson = File.ReadAllText(FILE_PATH);
            }
            finally
            {
                _readerWriterLock.ExitReadLock();
            }

            _projects = DeserializeObject<List<Project>>(projectjson)
                ?? new List<Project>();

            return _projects;

        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
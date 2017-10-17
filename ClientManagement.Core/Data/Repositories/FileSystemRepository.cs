using ClientManagement.Core.Interfaces;
using ClientManagement.Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static Newtonsoft.Json.JsonConvert;

namespace ClientManagement.Core.Data.Repositories
{
    public class FileSystemRepository : IEmployeeRepository
    {
        private readonly string FILE_PATH = ConfigurationManager.AppSettings["ClientEmployeeFilePath"];
        private IEnumerable<Employee> _employees;
        private static ReaderWriterLockSlim _readerWriterLock = new ReaderWriterLockSlim();
        private List<Project> _projects;

        public async Task Create(Employee employee)
        {
            var employees = (await GetAllEmployees()).ToList();

            employees.Add(employee);
            await PersistEmployees();
        }

        public Task<List<Employee>> GetAllEmployees(bool isSummary = false)
        {
            if (_employees != null)
                return Task.FromResult(_employees.ToList());

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

            return Task.FromResult(_employees.ToList());
        }

        public async Task<Employee> GetEmployee(Guid id)
        {
            var employees = await GetAllEmployees();
            var employee = employees.FirstOrDefault(x => x.Id == x.Id);

            return employee;
        }

        public async Task Update(Employee employee)
        {
            var employees = await GetAllEmployees();
            var employeeEntity = employees.FirstOrDefault(x => x.Id == employee.Id);

            if (employeeEntity == null)
                throw new InvalidOperationException("Unknown employee");

            employeeEntity.FirstName = employee.FirstName;
            employeeEntity.LastName = employee.LastName;
            employeeEntity.Gender = employee.Gender;

            await PersistEmployees();
        }

        private async Task PersistEmployees()
        {
            List<Employee> employees = (await GetAllEmployees()).ToList();
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

        public async Task AssignProjectToEmployee(Guid employeeId, Guid projectId)
        {
            var Projects = GetAllProjects();
            var project = Projects.FirstOrDefault(x => x.Id == projectId);
            var employee = await GetEmployee(employeeId);

            employee.Projects.Add(project);

            await PersistEmployees();
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

        public Task Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
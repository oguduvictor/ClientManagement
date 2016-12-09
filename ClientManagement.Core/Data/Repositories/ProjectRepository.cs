using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientManagement.Core.Models;
using ClientManagement.Core.Data.Db;
using System.Data.Entity;

namespace ClientManagement.Core.Data.Repositories
{
    public class ProjectRepository : IProjectRepository, IDisposable
    {
        private readonly DbManagementContext _context;
        private readonly bool _externalContext;
        public ProjectRepository()
        {
            _context = new DbManagementContext();
        }
       public ProjectRepository(DbManagementContext context)
        {
            _context = context;
            _externalContext = true;
        }
       
        public void Create(Project project)
        {
            _context.Projects.Add(project);
            _context.SaveChanges();
        }

        public List<Project> GetAllProjects()
        {
            return _context.Projects.ToList();
        }

        public void Update(Project project)
        {
            if(!_context.Projects.Local.Contains(project))
            {
                _context.Projects.Attach(project);
                _context.Entry(project).State = EntityState.Modified;
            }

            _context.SaveChanges();
        }

        public List<Employee> GetEmployeeListForProject(int ProjectId)
        {
            return _context.Projects.Find(ProjectId).Employees.ToList();
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

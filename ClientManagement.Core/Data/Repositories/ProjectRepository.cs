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
    public class ProjectRepository : IProjectRepository
    {
        private readonly ProjectManagementContext _context;
        private readonly bool _externalContext;
        public ProjectRepository()
        {
            _context = new ProjectManagementContext();
        }
        public ProjectRepository(ProjectManagementContext context)
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
    }
}

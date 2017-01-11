using System;
using System.Collections.Generic;
using System.Linq;
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
            return _context.Projects.Include(x => x.Client).ToList();
        }
        public Project GetProject(Guid id)
        {
            return GetAllProjects().FirstOrDefault(x => x.Id == id);
        }
        public void Update(Project project)
        {
            var dbProject = GetProject(project.Id);

            dbProject.Title = project.Title;
            dbProject.Description = project.Description;
            dbProject.ProjectStatus = project.ProjectStatus;

            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var project = GetProject(id);
            _context.Projects.Remove(project);
            _context.Entry(project).State = EntityState.Deleted;
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

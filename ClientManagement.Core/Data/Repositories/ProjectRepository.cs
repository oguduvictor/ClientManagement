﻿using ClientManagement.Core.Data.Db;
using ClientManagement.Core.Interfaces;
using ClientManagement.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task Create(Project project)
        {
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();
        }
        public Task<List<Project>> GetAllProjects(bool isSummary = false)
        {

            var query = _context.Projects.AsQueryable();

            if (isSummary)
            {
                return query.Select(x => new { x.Id, x.Title })
                    .ToListAsync()
                    .ContinueWith(items => items.Result.Select(x => new Project
                    {
                        Id = x.Id,
                        Title = x.Title
                    }).ToList());
            }

            return query.ToListAsync();
        }
        public async Task<Project> GetProject(Guid id)
        {
            return await _context.Projects.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task Update(Project project)
        {
            var dbProject = await GetProject(project.Id);

            dbProject.Title = project.Title;
            dbProject.Description = project.Description;
            dbProject.Status = project.Status;

            _context.SaveChanges();
        }

        public async Task Delete(Guid id)
        {
            var project = await GetProject(id);
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

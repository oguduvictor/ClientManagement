using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClientManagement.Core.Data.Db;
using ClientManagement.Core.Models;
using ClientManagement.Core.Services;

namespace ClientManagement.Web.Controllers
{
    public class ProjectController : Controller
    {
        private DbManagementContext db = new DbManagementContext();
        private IProjectService _projectService;

        public ProjectController()
        {
            _projectService = new ProjectService();
        }

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        // GET: Project
        public ActionResult Index()
        {
            var projects = _projectService.GetAllProjects();
            return View(projects);
        }

        // GET: Project/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var project = _projectService.GetProject(id.Value);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // GET: Project/Create
        public ActionResult Create()
        {
            ViewBag.ClientId = new SelectList(db.Clients, "Id", "Name");
            return View();
        }

        // POST: Project/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Description,ClientId,ProjectStatus")] Project project)
        {
            if (ModelState.IsValid)
            {
                _projectService.Save(project);
                return RedirectToAction("Index");
            }
            var clientIds = project.Client.Name;
            ViewBag.ClientId = new SelectList(clientIds, "Id", "Name", project.ClientId);
            return View(project);
        }

        // GET: Project/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = _projectService.GetProject(id.Value);
            if (project == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClientId = new SelectList(db.Clients, "Id", "Name", project.ClientId);
            return View(project);
        }
        // POST: Project/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Description,ClientId,ProjectStatus")] Project project)
        {
            if (ModelState.IsValid)
            {
                _projectService.Save(project);
                return RedirectToAction("Index");
            }
            ViewBag.ClientId = new SelectList(project.Client.Name, "Id", "Name", project.ClientId);
            return View(project);
        }

        // GET: Project/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = _projectService.GetProject(id.Value);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Project/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _projectService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}

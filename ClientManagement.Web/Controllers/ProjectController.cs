using System.Linq;
using System.Net;
using System.Web.Mvc;
using ClientManagement.Core.Models;
using ClientManagement.Core.Services;
using System;

namespace ClientManagement.Web.Controllers
{
    [Authorize(Roles = "Manager")]
    public class ProjectController : Controller
    {
        private readonly IProjectService _projectService;
        private readonly IClientService _clientService;
       
        public ProjectController(IProjectService projectService, IClientService clientService)
        {
            _projectService = projectService;
            _clientService = clientService;
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
        public ActionResult Details(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var project = _projectService.GetProject(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            var clients = _clientService.GetAllClients();
            ViewBag.ClientName = clients.FirstOrDefault(x=>x.Id == project.ClientId).Name;
            return View(project);
        }

        public ActionResult ProjectEmployees(Guid id)
        {
            var project = _projectService.GetProject(id);
            var projectEmployees = _projectService.GetEmployeeListForProject(id);
            ViewBag.Project = project.Title;
            return View(projectEmployees);
        }

        // GET: Project/Create
        public ActionResult Create()
        {
            ViewBag.ClientId = new SelectList(_clientService.GetAllClients(), "Id", "Name");
            return View();
        }

        // POST: Project/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Description,ClientId,ProjectStatus")] Project project)
        {
            if (ModelState.IsValid)
            {
                project.Id = Guid.NewGuid();
                _projectService.Save(project);
                return RedirectToAction("Index");
            }
            ViewBag.ClientId = new SelectList(_clientService.GetAllClients(), "Id", "Name", project.ClientId);
            return View(project);
        }

        // GET: Project/Edit/5
        public ActionResult Edit(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var project = _projectService.GetProject(id);
            
            if (project == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClientId = new SelectList(_clientService.GetAllClients(), "Id", "Name", project.ClientId);
            return View(project);
        }
        // POST: Project/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Title,Description,ClientId,ProjectStatus")] Project project)
        {
            if (ModelState.IsValid)
            {
                _projectService.Save(project);
                return RedirectToAction("Index");
            }
            ViewBag.ClientId = new SelectList(_clientService.GetAllClients(), "Id", "Name", project.ClientId);
            return View(project);
        }

        // GET: Project/Delete/5
        public ActionResult Delete(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var project = _projectService.GetProject(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Project/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            _projectService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}